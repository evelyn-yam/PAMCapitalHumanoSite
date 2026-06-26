// using Template.Entities; // PLANTILLA AUTH: AuthConstants.AccessTokenKey
using Template.Modules.HttpClient;
using Template.Persistence.IPersistence;
// using Blazored.LocalStorage; // PLANTILLA AUTH: SESSION_STOLEN
// using Microsoft.AspNetCore.Components; // PLANTILLA AUTH: NavigationManager
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Template.Persistence
{
    /// <summary>
    /// Implementacion comun para ejecutar requests HTTP contra clientes nombrados.
    /// Maneja serializacion JSON, headers, bearer token y conversion centralizada de errores.
    /// Los repositorios deben usar esta clase en lugar de crear HttpClient manualmente.
    /// </summary>
    /// <example>
    /// <code>
    /// var result = await _clientFactory.GetItemsAsync&lt;MyDto&gt;(
    ///     "ApiService",
    ///     "./v1/resource",
    ///     token,
    ///     new Dictionary&lt;string, string&gt;());
    /// </code>
    /// </example>
    public class ClientFactory : IClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        // PLANTILLA AUTH: necesarios para cerrar sesion ante SESSION_STOLEN (401)
        // private readonly NavigationManager _navigationManager;
        // private readonly ILocalStorageService _localStorage;

        public ClientFactory(
            IHttpClientFactory httpClientFactory)
            // NavigationManager navigationManager, // PLANTILLA AUTH
            // ILocalStorageService localStorage) // PLANTILLA AUTH
        {
            _httpClientFactory = httpClientFactory;
            // _navigationManager = navigationManager;
            // _localStorage = localStorage;
        }

        // Helpers internos para mantener consistente el formato de cada request.
        private static void AddBearer(HttpClient client, string token)
        {
            if (!string.IsNullOrWhiteSpace(token) && token != "NA")
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
        }

        private static StringContent ToJsonContent<T>(T payload)
        {
            var json = JsonConvert.SerializeObject(payload,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static void AddHeaders(HttpClient client, Dictionary<string, string>? headers)
        {
            if (headers == null) return;

            foreach (var header in headers)
                client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
        }

        /// <summary>
        /// Envia una solicitud HTTP usando un metodo dinamico.
        /// Usar cuando GET/POST/PUT/PATCH/DELETE no cubren el caso.
        /// </summary>
        public async Task<T> SendItemAsync<T, U>(
            string tagHttpClient,
            HttpMethod httpMethod,
            string url,
            U payload,
            string token)
        {
            var client = _httpClientFactory.CreateClient(tagHttpClient);

            AddBearer(client, token);

            var request = new HttpRequestMessage(httpMethod, url)
            {
                Content = ToJsonContent(payload)
            };

            var response = await client.SendAsync(request);
            return await HandleResponse<T>(response);
        }

        /// <summary>
        /// Ejecuta una solicitud GET y deserializa la respuesta al tipo indicado.
        /// </summary>
        public async Task<T> GetItemsAsync<T>(
            string tagHttpClient,
            string url,
            string token,
            Dictionary<string, string> headers)
            where T : class
        {
            var client = _httpClientFactory.CreateClient(tagHttpClient);

            AddBearer(client, token);
            AddHeaders(client, headers);

            var response = await client.GetAsync(url);
            return await HandleResponse<T>(response);
        }

        /// <summary>
        /// Ejecuta una solicitud POST con payload JSON y deserializa la respuesta.
        /// </summary>
        public async Task<T> PostItemAsync<T, U>(
            string tagHttpClient,
            string url,
            U payload,
            string token,
            Dictionary<string, string> headers)
            where T : class
            where U : class
        {
            var client = _httpClientFactory.CreateClient(tagHttpClient);

            AddBearer(client, token);
            AddHeaders(client, headers);

            var response = await client.PostAsync(url, ToJsonContent(payload));
            return await HandleResponse<T>(response);
        }

        /// <summary>
        /// Ejecuta una solicitud PUT con payload JSON y deserializa la respuesta.
        /// </summary>
        public async Task<T> PutItemAsync<T, U>(
            string tagHttpClient,
            string url,
            U payload,
            string token)
            where T : class
            where U : class
        {
            var client = _httpClientFactory.CreateClient(tagHttpClient);

            AddBearer(client, token);

            var response = await client.PutAsync(url, ToJsonContent(payload));
            return await HandleResponse<T>(response);
        }

        /// <summary>
        /// Ejecuta una solicitud PATCH con payload JSON y deserializa la respuesta.
        /// </summary>
        public async Task<T> PatchItemAsync<T, U>(
            string tagHttpClient,
            string url,
            U payload,
            string token)
            where T : class
            where U : class
        {
            var client = _httpClientFactory.CreateClient(tagHttpClient);

            AddBearer(client, token);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
            {
                Content = ToJsonContent(payload)
            };

            var response = await client.SendAsync(request);
            return await HandleResponse<T>(response);
        }

        /// <summary>
        /// Ejecuta una solicitud DELETE y deserializa la respuesta.
        /// </summary>
        public async Task<T> DeleteItemAsync<T>(
            string tagHttpClient,
            string url,
            string token)
            where T : class
        {
            var client = _httpClientFactory.CreateClient(tagHttpClient);

            AddBearer(client, token);

            var response = await client.DeleteAsync(url);
            return await HandleResponse<T>(response);
        }

        // Punto unico de manejo de respuestas para mantener errores consistentes.
        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(json)!;

            // PLANTILLA AUTH: si el backend invalida la sesion, borra el token y redirige al login
            // if (response.StatusCode == HttpStatusCode.Unauthorized)
            // {
            //     try
            //     {
            //         var error = JsonConvert.DeserializeObject<dynamic>(json);
            //         if (error?.codigo == "SESSION_STOLEN")
            //         {
            //             await _localStorage.RemoveItemAsync(AuthConstants.AccessTokenKey);
            //             _navigationManager.NavigateTo($"{_navigationManager.BaseUri}?error=session_stolen", true);
            //         }
            //     }
            //     catch { /* fallback to standard exception */ }
            // }

            throw BuildException(response.StatusCode, json);
        }

        // Traduce el formato de error del backend a una excepcion consumible por la app.
        private static Exception BuildException(HttpStatusCode status, string json)
        {
            try
            {
                var error = JsonConvert.DeserializeObject<ApiErrorResponse>(json);

                var message =
                    error?.errores?.FirstOrDefault()
                    ?? error?.mensaje
                    ?? "Error inesperado del servidor";

                return new ApiException(message, (int)status);
            }
            catch
            {
                return new ApiException("Error inesperado del servidor", (int)status);
            }
        }
    }

}
