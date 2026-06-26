using Template.Entities;
using Template.Services.IServices.Auth;
using Blazored.LocalStorage;

namespace Template.Services.Auth
{
    /// <summary>
    /// Encapsula el acceso al token JWT en localStorage.
    /// Usar este servicio evita que componentes y repositorios conozcan las claves de almacenamiento.
    /// </summary>
    public sealed class AccessTokenService : IAccessTokenService
    {
        private readonly ILocalStorageService _localStorage;

        public AccessTokenService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string?> GetTokenAsync() =>
            await _localStorage.GetItemAsync<string?>(AuthConstants.AccessTokenKey);

        public async Task SetTokenAsync(string token, bool rememberMe = true)
        {
            await _localStorage.SetItemAsync(AuthConstants.AccessTokenKey, token);
            await _localStorage.SetItemAsync(AuthConstants.RememberMeKey, rememberMe);
        }

        public async Task ClearTokenAsync()
        {
            await _localStorage.RemoveItemAsync(AuthConstants.AccessTokenKey);
            await _localStorage.RemoveItemAsync(AuthConstants.RememberMeKey);
        }
    }
}
