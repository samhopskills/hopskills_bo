using HopSkills.Components;
using HopSkills.Plugins.InMemory;
using HopSkills.UseCases.Customers;
using HopSkills.UseCases.Customers.Interfaces;
using HopSkills.UseCases.Content;
using HopSkills.UseCases.Content.Interface;
using HopSkills.UseCases.PluginInterfaces;
using HopSkills.UseCases.Users;
using HopSkills.UseCases.Users.Interfaces;
using MudBlazor.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMudServices();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ITeamRepository, TeamRepository>();
builder.Services.AddSingleton<IRoleRepository, RoleRepository>();
builder.Services.AddSingleton<ITrainingRepository, TrainingRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IViewUserListUseCase, ViewUserListUseCase>();
builder.Services.AddTransient<IViewTeamListUseCase, ViewTeamListUseCase>();
builder.Services.AddTransient<IViewRoleListUseCase, ViewRoleListUseCase>();
builder.Services.AddTransient<IViewTrainingListUseCase, ViewTrainingListUseCase>();
builder.Services.AddTransient<IAddUserUseCase, AddUserUseCase>();
builder.Services.AddTransient<IViewCustomerListUseCase, ViewCustomerListUseCase>();
builder.Services.AddTransient<IAddCustomerUseCase, AddCustomerUseCase>();
builder.Services.AddTransient<IAddTeamUseCase, AddTeamUseCase>();
builder.Services.AddTransient<IAddRoleUseCase, AddRoleUseCase>();
builder.Services.AddTransient<IEditCustomerUseCase, EditCustomerUseCase>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();
