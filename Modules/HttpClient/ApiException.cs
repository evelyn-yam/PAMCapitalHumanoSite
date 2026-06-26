namespace Template.Modules.HttpClient
{
    /// <summary>
    /// Excepcion de dominio para errores devueltos por APIs externas.
    /// Conserva el StatusCode y una lista de mensajes para que la UI o los servicios
    /// puedan mostrar errores controlados sin depender de HttpResponseMessage.
    /// </summary>
    /// <example>
    /// <code>
    /// throw new ApiException("No autorizado", 401);
    /// </code>
    /// </example>
    public class ApiException : Exception
    {
        public int StatusCode { get; }
        public List<string> Errores { get; }

        public ApiException(string mensaje, int statusCode, List<string>? errores = null)
            : base(mensaje)
        {
            StatusCode = statusCode;
            Errores = errores ?? new List<string>();
        }
    }
}
