using Newtonsoft.Json;

namespace Template.Dtos.Auth
{
    /// <summary>
    /// Payload esperado por el endpoint de registro.
    /// Agregar aqui campos adicionales si el backend requiere nombre, telefono u otros datos.
    /// </summary>
    public sealed class SignUpRequestDto
    {
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;
    }
}
