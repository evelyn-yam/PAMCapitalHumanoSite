using Template.Persistence;
using Template.Persistence.IPersistence;
// using Template.Services.Auth; // PLANTILLA AUTH
using Template.Services.IServices;
// using Template.Services.IServices.Auth; // PLANTILLA AUTH
using Template.Services.Users;

namespace Template.Modules.Injection
{
    /// <summary>
    /// Centraliza el registro de dependencias de la aplicacion.
    /// Mantener aqui servicios, repositorios y unidades de trabajo evita registros dispersos en Program.cs.
    /// </summary>
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClientFactory, ClientFactory>();

            // Repositories
            // PLANTILLA AUTH: descomentar cuando exista API de login/registro
            // services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            // PLANTILLA AUTH: token en localStorage y casos de uso de autenticacion
            // services.AddScoped<IAccessTokenService, AccessTokenService>();
            // services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsersService, UsersService>();

            return services;
        }
    }
}
