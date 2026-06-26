namespace Template.Entities
{
    /// <summary>
    /// Entidad de usuario usada como ejemplo de dominio para el template.
    /// No contiene atributos JSON porque no depende directamente del contrato de la API.
    /// </summary>
    /// <example>
    /// <code>
    /// var user = new User
    /// {
    ///     Id = "1",
    ///     Name = "Maria",
    ///     LastName = "Lopez",
    ///     Email = "maria@empresa.com",
    ///     Role = "Administrador",
    ///     Phone = "9981234567",
    ///     Status = "Activo",
    ///     Ubication = "Mexico"
    /// };
    /// </code>
    /// </example>
    public sealed class User
    {
        public string? Id { get; set; }

        public string? Ubication { get; set; }

        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? Role { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Status { get; set; }
    }
}
