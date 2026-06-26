using Template.Entities;

namespace Template.Services.IServices
{
    /// <summary>
    /// Contrato publico para los casos de uso de usuarios.
    /// Los componentes Razor deben depender de esta interfaz y no de implementaciones concretas.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Obtiene el listado de usuarios desde el backend.
        /// </summary>
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
