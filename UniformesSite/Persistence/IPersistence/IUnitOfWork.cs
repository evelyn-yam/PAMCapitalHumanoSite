namespace Template.Persistence.IPersistence
{
    /// <summary>
    /// Expone los repositorios disponibles para los servicios de aplicacion.
    /// Agregar nuevos repositorios aqui cuando formen parte del acceso comun a API.
    /// </summary>
    public interface IUnitOfWork
    {
        // PLANTILLA AUTH: repositorio de login/registro contra la API
        // IAuthRepository AuthRepository { get; }

        IUsersRepository UsersRepository { get; }
    }
}
