namespace Template.Modules.HttpClient
{
    /// <summary>
    /// Modelo minimo esperado para respuestas de error del backend.
    /// Se usa al construir ApiException desde ClientFactory.
    /// </summary>
    /// <example>
    /// <code>
    /// { "mensaje": "Solicitud invalida", "errores": ["El campo dni es requerido"] }
    /// </code>
    /// </example>
    public class ApiErrorResponse
    {
        public string? mensaje { get; set; }
        public List<string>? errores { get; set; }
    }
}
