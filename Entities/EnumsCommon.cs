namespace Template.Entities
{
    /// <summary>
    /// Contenedor de enumeraciones compartidas por la aplicacion.
    /// Usar este archivo para valores transversales que deben mantenerse tipados
    /// y evitar cadenas duplicadas entre modulos.
    /// </summary>
    public class EnumsCommon
    {

        /// <summary>
        /// Nombres de HttpClient registrados en IHttpClientFactory.
        /// El valor debe coincidir con el nombre usado en HttpClientExtensions.
        /// </summary>
        /// <example>
        /// <code>
        /// var tag = nameof(EnumsCommon.TagHttpClientFactory.ApiService);
        /// </code>
        /// </example>
        public enum TagHttpClientFactory
        {
            ApiService
        }
        
    }
}
