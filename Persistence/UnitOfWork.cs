using Template.Persistence.IPersistence;

namespace Template.Persistence
{
    /// <summary>
    /// Agrupa repositorios relacionados para que los servicios consuman una sola dependencia.
    /// En Blazor WASM no administra transacciones de base de datos; funciona como fachada de acceso a API.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        // PLANTILLA AUTH: exponer AuthRepository al AuthService
        // public IAuthRepository AuthRepository { get; }

        public IUsersRepository UsersRepository { get; }

        public UnitOfWork(
            // IAuthRepository authRepository, // PLANTILLA AUTH
            IUsersRepository usersRepository)
        {
            // AuthRepository = authRepository;
            UsersRepository = usersRepository;
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
