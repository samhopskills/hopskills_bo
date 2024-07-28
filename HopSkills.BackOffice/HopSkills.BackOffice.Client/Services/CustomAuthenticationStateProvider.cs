using HopSkills.BO.CoreBusiness;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Json;
using HopSkills.BackOffice.Client.ViewModels;
namespace HopSkills.BackOffice.Client.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IAccountManagement
    {
        private bool _authenticated = false;
        private readonly ILogger<CustomAuthenticationStateProvider> _logger;
        private readonly ClaimsPrincipal Unauthenticated =
           new(new ClaimsIdentity());

        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions jsonSerializerOptions =
          new()
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          };
        public CustomAuthenticationStateProvider(IHttpClientFactory httpClientFactory,
            ILogger<CustomAuthenticationStateProvider> logger)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
            _logger = logger;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddRole(string roleName)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(roleName), Encoding.UTF8, 
                    "application/json");
                var result = await _httpClient.PostAsync("api/Role/addRole", content);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return false;
        }

        public Task<List<Role>> GetRoles()
        {
            throw new NotImplementedException();
        }
    }
}
