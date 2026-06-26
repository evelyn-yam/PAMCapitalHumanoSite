using Microsoft.Extensions.Options;
using Template.Dtos.Auth;
using Template.Entities;
using Template.Modules.Features;
using Template.Persistence.IPersistence;

namespace Template.Persistence
{
    /// <summary>
    /// Repositorio HTTP para endpoints de autenticacion.
    /// Las rutas se leen desde AuthOptions para que cada ambiente pueda apuntar a endpoints distintos.
    /// </summary>
    public sealed class AuthRepository : IAuthRepository
    {
        private readonly IClientFactory _clientFactory;
        private readonly AuthOptions _authOptions;

        public AuthRepository(IClientFactory clientFactory, IOptions<AuthOptions> authOptions)
        {
            _clientFactory = clientFactory;
            _authOptions = authOptions.Value;
        }

        public Task<SignInResponseDto> SignInAsync(SignInRequestDto request) =>
            PostAuthAsync(_authOptions.SignInPath, request);

        public Task<SignInResponseDto> SignUpAsync(SignUpRequestDto request) =>
            PostAuthAsync(_authOptions.SignUpPath, request);

        private Task<SignInResponseDto> PostAuthAsync<TPayload>(string url, TPayload payload)
            where TPayload : class
        {
            var tag = nameof(EnumsCommon.TagHttpClientFactory.ApiService);

            return _clientFactory.PostItemAsync<SignInResponseDto, TPayload>(
                tag,
                url,
                payload,
                string.Empty,
                new Dictionary<string, string>());
        }
    }
}
