namespace Template.Persistence.IPersistence
{
    /// <summary>
    /// Contrato comun para ejecutar llamadas HTTP desde repositorios.
    /// Los metodos reciben el nombre del HttpClient, la ruta relativa, el token y headers opcionales.
    /// </summary>
    /// <example>
    /// <code>
    /// await _clientFactory.GetItemsAsync&lt;MyResponse&gt;(
    ///     "ApiService",
    ///     "./v1/items",
    ///     token,
    ///     new Dictionary&lt;string, string&gt;());
    /// </code>
    /// </example>
    public interface IClientFactory
    {
        /// <summary>
        /// Envia una solicitud con metodo HTTP dinamico.
        /// </summary>
        Task<T> SendItemAsync<T, U>(
            string tagHttpClient,
            HttpMethod httpMethod,
            string eventHubName,
            U payload,
            string strToken);

        /// <summary>
        /// Ejecuta una solicitud GET.
        /// </summary>
        Task<T> GetItemsAsync<T>(
            string tagHttpClient,
            string eventHubName,
            string strToken,
            Dictionary<string, string> headers)
            where T : class;

        /// <summary>
        /// Ejecuta una solicitud POST con payload JSON.
        /// </summary>
        Task<T> PostItemAsync<T, U>(
            string tagHttpClient,
            string eventHubName,
            U payload,
            string strToken,
            Dictionary<string, string> headers)
            where T : class
            where U : class;

        /// <summary>
        /// Ejecuta una solicitud PUT con payload JSON.
        /// </summary>
        Task<T> PutItemAsync<T, U>(
            string tagHttpClient,
            string eventHubName,
            U payload,
            string strToken)
            where T : class
            where U : class;

        /// <summary>
        /// Ejecuta una solicitud PATCH con payload JSON.
        /// </summary>
        Task<T> PatchItemAsync<T, U>(
            string tagHttpClient,
            string eventHubName,
            U payload,
            string strToken)
            where T : class
            where U : class;

        /// <summary>
        /// Ejecuta una solicitud DELETE.
        /// </summary>
        Task<T> DeleteItemAsync<T>(
            string tagHttpClient,
            string eventHubName,
            string strToken)
            where T : class;
    }

}
