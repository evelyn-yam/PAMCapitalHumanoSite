using Template.Dtos.Users;

namespace Template.Persistence.IPersistence
{
    /// <summary>
    /// Contrato de persistencia para endpoints de usuarios.
    /// Define las llamadas HTTP disponibles sin exponer rutas a la capa de servicios.
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Consulta el endpoint de usuarios y devuelve DTOs listos para mapearse en la UI o servicio.
        /// </summary>
        Task<IEnumerable<UserDto>> GetUsersAsync();
    }
}
