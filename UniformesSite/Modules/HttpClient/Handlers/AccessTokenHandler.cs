using System.Net;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Template.Entities;

namespace Template.Modules.HttpClient.Handlers
{
    /// <summary>
    /// Handler HTTP que agrega el header Authorization Bearer cuando existe un token guardado.
    /// Se conecta al HttpClient nombrado para evitar repetir la logica de autenticacion en cada request.
    /// </summary>
    /// <example>
    /// <code>
    /// services.AddHttpClient("ApiService").AddHttpMessageHandler&lt;AccessTokenHandler&gt;();
    /// </code>
    /// </example>
    public class AccessTokenHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public AccessTokenHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>(AuthConstants.AccessTokenKey);

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
