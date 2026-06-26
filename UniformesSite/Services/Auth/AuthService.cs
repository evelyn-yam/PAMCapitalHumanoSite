// =============================================================================
// PLANTILLA AUTH — Este archivo NO se compila (ver Template.csproj Compile Remove).
// Guia de activacion: docs/PLANTILLA_AUTH.md
// =============================================================================

using Microsoft.AspNetCore.Components.Authorization;
using Template.Dtos.Auth;
using Template.Modules.HttpClient;
using Template.Persistence.IPersistence;
using Template.Services.IServices.Auth;

namespace Template.Services.Auth
{
    /// <summary>
    /// Servicio de aplicacion para autenticacion JWT.
    /// Coordina repositorio, almacenamiento local del token y notificacion del estado de sesion.
    /// </summary>
    public sealed class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessTokenService _accessTokenService;
        private readonly JwtAuthStateProvider _authStateProvider;

        public AuthService(
            IUnitOfWork unitOfWork,
            IAccessTokenService accessTokenService,
            AuthenticationStateProvider authStateProvider)
        {
            _unitOfWork = unitOfWork;
            _accessTokenService = accessTokenService;
            _authStateProvider = (JwtAuthStateProvider)authStateProvider;
        }

        public async Task SignInAsync(string email, string password, bool rememberMe)
        {
            await _accessTokenService.ClearTokenAsync();

            var response = await _unitOfWork.AuthRepository.SignInAsync(new SignInRequestDto
            {
                Email = email.Trim(),
                Password = password,
                RememberMe = rememberMe
            });

            await PersistSessionAsync(response, rememberMe);
        }

        public async Task SignUpAsync(string email, string password)
        {
            await _accessTokenService.ClearTokenAsync();

            var response = await _unitOfWork.AuthRepository.SignUpAsync(new SignUpRequestDto
            {
                Email = email.Trim(),
                Password = password
            });

            await PersistSessionAsync(response, rememberMe: true);
        }

        public async Task SignOutAsync()
        {
            await _accessTokenService.ClearTokenAsync();
            _authStateProvider.NotifyUserLogout();
        }

        private async Task PersistSessionAsync(SignInResponseDto response, bool rememberMe)
        {
            var token = response.ResolveAccessToken();

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ApiException("La API no devolvio un token de acceso.", 500);
            }

            await _accessTokenService.SetTokenAsync(token, rememberMe);
            _authStateProvider.NotifyUserAuthentication();
        }
    }
}
