using Template.Entities;
using Template.Mappers.Users;
using Template.Persistence.IPersistence;
using Template.Services.IServices;

namespace Template.Services.Users
{
    /// <summary>
    /// Servicio de aplicacion para casos de uso de usuarios.
    /// Mantiene a los componentes Razor desacoplados de repositorios y detalles HTTP.
    /// </summary>
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _unitOfWork.UsersRepository.GetUsersAsync();
            return UserMapper.ToEntities(users);
        }
    }
}
