using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Template.Services.IServices.Auth;

namespace Template.Services.Auth
{
    /// <summary>
    /// Expone el estado de autenticacion a Blazor a partir del token en localStorage.
    /// </summary>
    public sealed class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAccessTokenService _accessTokenService;

        public JwtAuthStateProvider(IAccessTokenService accessTokenService)
        {
            _accessTokenService = accessTokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _accessTokenService.GetTokenAsync();

            if (string.IsNullOrWhiteSpace(token) || JwtTokenParser.IsExpired(token))
            {
                if (!string.IsNullOrWhiteSpace(token))
                {
                    await _accessTokenService.ClearTokenAsync();
                }

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var user = JwtTokenParser.CreatePrincipal(token);
            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void NotifyUserLogout()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
