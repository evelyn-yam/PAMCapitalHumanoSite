using Microsoft.Extensions.DependencyInjection;

namespace Template.Modules.Features
{
    /// <summary>
    /// Registra opciones de caracteristicas configurables por ambiente.
    /// Mantener opciones aqui permite activar modulos sin cargar Program.cs de detalles.
    /// </summary>
    public static class FeatureExtensions
    {
        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
        {
            // PLANTILLA AUTH: rutas de sign-in/sign-up configurables (AuthOptions en appsettings.json)
            // services.Configure<AuthOptions>(configuration.GetSection(AuthOptions.SectionName));

            return services;
        }
    }
}
