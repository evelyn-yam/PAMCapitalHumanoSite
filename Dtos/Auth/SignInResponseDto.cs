using Newtonsoft.Json;

namespace Template.Dtos.Auth
{
    /// <summary>
    /// Respuesta esperada despues de autenticarse.
    /// ResolveAccessToken permite soportar APIs que devuelven accessToken o token.
    /// </summary>
    public sealed class SignInResponseDto
    {
        [JsonProperty("accessToken")]
        public string? AccessToken { get; set; }

        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("refreshToken")]
        public string? RefreshToken { get; set; }

        public string? ResolveAccessToken() =>
            !string.IsNullOrWhiteSpace(AccessToken) ? AccessToken : Token;
    }
}
