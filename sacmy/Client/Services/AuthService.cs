using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using sacmy.Shared.Core;
using sacmy.Shared.ViewModels.EmployeeViewModel;
using sacmy.Shared.ViewModels.UserViewModel;

namespace sacmy.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly UserGlobalClass _userGlobal;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, UserGlobalClass userGlobal)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _userGlobal = userGlobal;
        }

        public async Task<UserViewModel> SignInAsync(SigninPostRequestViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/signin", model);
            response.EnsureSuccessStatusCode();

            var user = await response.Content.ReadFromJsonAsync<UserViewModel>();
            if (user != null)
            {
                // Store user in the global state
                _userGlobal.User = user;

                // Save user to local storage for persistence
                await _localStorage.SetItemAsync("currentUser", user);

                // Store auth status
                await _localStorage.SetItemAsync("isAuthenticated", true);
            }

            return user;
        }

        public async Task<bool> IsUserAuthenticatedAsync()
        {
            var isAuthenticated = await _localStorage.GetItemAsync<bool>("isAuthenticated");
            if (isAuthenticated)
            {
                var user = await _localStorage.GetItemAsync<UserViewModel>("currentUser");
                if (user != null)
                {
                    _userGlobal.User = user;
                    return true;
                }
            }

            return false;
        }

        public async Task SignOutAsync()
        {
            await _localStorage.RemoveItemAsync("currentUser");
            await _localStorage.SetItemAsync("isAuthenticated", false);
            _userGlobal.User = new UserViewModel();
        }
    }
}
