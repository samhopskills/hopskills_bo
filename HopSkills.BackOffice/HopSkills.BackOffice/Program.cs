using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using HopSkills.BackOffice.Client.Pages;
using HopSkills.BackOffice.Components;
using HopSkills.BackOffice.Components.Account;
using HopSkills.BackOffice.Data;
using HopSkills.BO.UseCases.Content.Interface;
using HopSkills.BO.UseCases.Content;
using HopSkills.BO.UseCases.Customers.Interfaces;
using HopSkills.BO.UseCases.Customers;
using HopSkills.BO.UseCases.PluginInterfaces;
using HopSkills.BO.UseCases.Users.Interfaces;
using HopSkills.BO.UseCases.Users;
using HopSkills.BO.Plugins.InMemory;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HopSkills.BackOffice.Services.Interfaces;
using HopSkills.BackOffice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<HopSkillsDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HopSkillsDbContext>()
    .AddSignInManager()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddRoleStore<RoleStore<IdentityRole, HopSkillsDbContext>>()
    .AddDefaultTokenProviders();

//builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
//    .AddUserManager<UserManager<ApplicationUser>>()
//    .AddRoles<ApplicationRole>()
//    .AddEntityFrameworkStores<HopSkillsDbContext>()
//    .AddSignInManager()
//    .AddRoleManager<RoleManager<ApplicationRole>>()
//    .AddRoleStore<RoleStore<ApplicationRole, HopSkillsDbContext>>()
//    .AddDefaultTokenProviders();


builder.Services.AddControllers().AddNewtonsoftJson(); 
builder.Services.AddControllersWithViews();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ITeamRepository, TeamRepository>();
builder.Services.AddSingleton<IRoleRepository, RoleRepository>();
builder.Services.AddSingleton<ITrainingRepository, TrainingRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IViewUserListUseCase, ViewUserListUseCase>();
builder.Services.AddTransient<IViewTeamListUseCase, ViewTeamListUseCase>();
builder.Services.AddTransient<IViewRoleListUseCase, ViewRoleListUseCase>();
builder.Services.AddTransient<IViewTrainingListUseCase, ViewTrainingListUseCase>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();


builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

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

app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(HopSkills.BackOffice.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
