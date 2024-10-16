using Azure.Storage.Blobs;
using HopSkills.BackOffice.Components;
using HopSkills.BackOffice.Components.Account;
using HopSkills.BackOffice.Data;
using HopSkills.BackOffice.Services;
using HopSkills.BackOffice.Services.Interfaces;
using HopSkills.BO.Plugins.InMemory;
using HopSkills.BO.UseCases.PluginInterfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Serilog;
using System.Security.Claims;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();

builder.Services.AddDbContext<HopSkillsDbContext>(options =>
    options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped(x => new 
BlobServiceClient(configuration["ConnectionStrings:StorageAccount"]));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
                .CreateLogger();

builder.Services.AddLogging(logging => logging.AddSerilog(Log.Logger, true));

builder.Services.AddIdentityCore<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    })
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HopSkillsDbContext>()
    .AddSignInManager()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddRoleStore<RoleStore<IdentityRole, HopSkillsDbContext>>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureOptions<ConfigureSecurityStampOptions>();
builder.Services.AddControllers().AddNewtonsoftJson().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
}); ; 
builder.Services.AddControllersWithViews();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<ITrainingService, TrainingService>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7079/") });
builder.Services.AddScoped<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

//app.MapIdentityApi<ApplicationUser>();
app.UseCors(builder => builder
.AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(HopSkills.BackOffice.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapGet("/api/roles", async (HttpContext httpContext) =>
{
    var user = httpContext.User;
    if (user.Identity?.IsAuthenticated == true)
    {
        // Get roles from the claims
        var roles = user.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList();

        if (roles.Any())
        {
            return Results.Ok(roles);
        }
        return Results.NotFound("No roles found for this user.");
    }
    return Results.Unauthorized();
}).RequireAuthorization();



app.Run();
