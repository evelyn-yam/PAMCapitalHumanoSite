using Template.Dtos.Users;
using Template.Entities;

namespace Template.Mappers.Users
{
    /// <summary>
    /// Convierte contratos de API de usuarios en entidades de dominio.
    /// Mantener este mapeo fuera del servicio evita mezclar transporte HTTP con reglas internas de la app.
    /// </summary>
    public static class UserMapper
    {
        public static User ToEntity(UserDto dto)
        {
            return new User
            {
                Id = Normalize(dto.Id),
                Name = Normalize(dto.Name),
                LastName = Normalize(dto.LastName),
                Email = Normalize(dto.Email),
                Role = Normalize(dto.Role),
                Phone = Normalize(dto.Phone),
                Status = NormalizeStatus(dto.Status),
                Ubication = Normalize(dto.Ubication)
            };
        }

        public static IReadOnlyList<User> ToEntities(IEnumerable<UserDto> dtos)
        {
            return dtos.Select(ToEntity).ToList();
        }

        private static string? Normalize(string? value)
        {
            var normalized = value?.Trim();
            return string.IsNullOrWhiteSpace(normalized) ? null : normalized;
        }

        private static string NormalizeStatus(string? status) => Normalize(status)?.ToLowerInvariant() switch
        {
            "activo" or "active" => "Activo",
            "pausado" or "paused" or "inactive" => "Pausado",
            "invitado" or "invited" or "pending" => "Invitado",
            _ => "Activo"
        };
    }
}
