using Template.Dtos.Users;
using Template.Entities;
using Template.Persistence.IPersistence;

namespace Template.Persistence
{
    /// <summary>
    /// Repositorio encargado de consultar endpoints relacionados con usuarios.
    /// La capa de persistencia conoce la ruta HTTP, el cliente nombrado y el DTO esperado.
    /// </summary>
    public class UsersRepository : IUsersRepository
    {
        private readonly IClientFactory _clientFactory;

        public UsersRepository(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var tag = nameof(EnumsCommon.TagHttpClientFactory.ApiService);
            // Usar ruta relativa para respetar el BaseAddress configurado.
            // Ejemplo: http://localhost:3000/api/ + ./v1/users.
            var url = "./v1/users";

            return await _clientFactory.GetItemsAsync<IEnumerable<UserDto>>(
                tag,
                url,
                string.Empty,
                new Dictionary<string, string>());
        }
    }
}
