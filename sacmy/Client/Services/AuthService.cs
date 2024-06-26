using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.UserViewModel;

namespace sacmy.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserGlobalClass _userGobalClass;
        public AuthService(IHttpClientFactory httpClientFactory , UserGlobalClass userGlobalClass)
        {
            _httpClientFactory = httpClientFactory;
            _userGobalClass = userGlobalClass;
        }

        public async Task<UserViewModel> SignInAsync(string phoneNumber, string password)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("sacmy.ServerAPI");
                var response = await client.PostAsJsonAsync("api/User/signin", new { phoneNumber, password }); 
                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<UserViewModel>();
                    _userGobalClass.SetUser(user);
                    return user;
                }
                else
                {
                    // Handle errors
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return null;
            }
        }
    }
}
