using System.ComponentModel.DataAnnotations;

namespace Template.ViewModels.Validation
{
    /// <summary>
    /// Valida que una coleccion tenga al menos una cantidad minima de elementos seleccionados.
    /// Util para multiselects y listas de permisos en modelos de vista.
    /// </summary>
    /// <example>
    /// <code>
    /// [MinSelectedItems(2, ErrorMessage = "Selecciona al menos 2 permisos.")]
    /// public string[] PermissionCodes { get; set; } = [];
    /// </code>
    /// </example>
    public sealed class MinSelectedItemsAttribute : ValidationAttribute
    {
        private readonly int minimum;

        public MinSelectedItemsAttribute(int minimum)
        {
            this.minimum = minimum;
        }

        public override bool IsValid(object? value)
        {
            return value is string[] items && items.Length >= minimum;
        }
    }
}
