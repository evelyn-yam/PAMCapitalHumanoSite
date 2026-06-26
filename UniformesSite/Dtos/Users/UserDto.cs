using Newtonsoft.Json;

namespace Template.Dtos.Users
{
    /// <summary>
    /// DTO esperado para el endpoint GET ./v1/users.
    /// Mantener este contrato separado de la entidad permite adaptar cambios de API sin contaminar dominio/UI.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "id": "1",
    ///   "ubication": "Mexico",
    ///   "email": "maria@empresa.com",
    ///   "name": "Maria",
    ///   "lastname": "Lopez",
    ///   "role": "Administrador",
    ///   "status": "Activo",
    ///   "phone": "9981234567"
    /// }
    /// </code>
    /// </example>
    public sealed class UserDto
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("ubication")]
        public string? Ubication { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("role")]
        public string? Role { get; set; }

        [JsonProperty("lastname")]
        public string? LastName { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }
    }
}
