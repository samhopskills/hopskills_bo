using HopSkills.BackOffice.Client;
using HopSkills.BO.UseCases.Content.Interface;
using HopSkills.BO.UseCases.Content;
using HopSkills.BO.UseCases.Customers.Interfaces;
using HopSkills.BO.UseCases.Customers;
using HopSkills.BO.UseCases.PluginInterfaces;
using HopSkills.BO.UseCases.Users.Interfaces;
using HopSkills.BO.UseCases.Users;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using HopSkills.BO.Plugins.InMemory;
using HopSkills.BackOffice.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
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

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

// set base address for default host
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7079") });

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

await builder.Build().RunAsync();
