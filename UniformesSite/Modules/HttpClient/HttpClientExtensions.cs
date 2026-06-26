// using Template.Modules.HttpClient.Handlers; // PLANTILLA AUTH: AccessTokenHandler

namespace Template.Modules.HttpClient
{
    /// <summary>
    /// Registra clientes HTTP nombrados para consumir APIs externas.
    /// Cada cliente debe tener su BaseAddress en configuracion para evitar URLs absolutas dentro de repositorios.
    /// </summary>
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpClient(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // PLANTILLA AUTH: handler que lee accessToken de localStorage y agrega Authorization Bearer
            //services.AddScoped<AccessTokenHandler>();

            var baseUrl = configuration["ExternalProviders:ApiService:UrlBase"];
            var normalizedBaseUrl = baseUrl?.TrimEnd('/') + "/";

            services.AddHttpClient("ApiService", client =>
            {
                client.BaseAddress = new Uri(normalizedBaseUrl);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("BLAZOR-SITE");
                client.Timeout = TimeSpan.FromMinutes(2);
            });
            // PLANTILLA AUTH: encadena el handler al cliente nombrado
            // .AddHttpMessageHandler<AccessTokenHandler>();

            return services;
        }
    }
}
