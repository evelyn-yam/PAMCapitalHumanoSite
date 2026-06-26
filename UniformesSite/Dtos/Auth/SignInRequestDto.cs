using Newtonsoft.Json;

namespace Template.Dtos.Auth
{
    /// <summary>
    /// Payload esperado por el endpoint de inicio de sesion.
    /// Mantener nombres JSON alineados con el contrato del backend.
    /// </summary>
    public sealed class SignInRequestDto
    {
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;

        [JsonProperty("rememberMe")]
        public bool RememberMe { get; set; }
    }
}
