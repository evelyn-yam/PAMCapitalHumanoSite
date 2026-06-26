using System.ComponentModel.DataAnnotations;
using Template.ViewModels.Validation;

namespace Template.ViewModels.Dashboard
{
    /// <summary>
    /// Modelo de vista usado por la pagina de perfil.
    /// Contiene solo estado de formulario y reglas de validacion de UI;
    /// no representa una entidad de dominio ni un contrato directo del backend.
    /// </summary>
    /// <example>
    /// <code>
    /// var model = new ProfileFormViewModel();
    /// var context = new EditContext(model);
    /// </code>
    /// </example>
    public sealed class ProfileFormViewModel
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 80 caracteres.")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "El correo electronico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Captura un correo electronico valido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El telefono es obligatorio.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El telefono debe tener exactamente 10 digitos.")]
        public string? Phone { get; set; }

        [Range(18, 99, ErrorMessage = "La edad debe estar entre 18 y 99.")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Selecciona un rol.")]
        public string? Role { get; set; }

        [MinSelectedItems(2, ErrorMessage = "Selecciona al menos 2 permisos.")]
        public string[] SkillCodes { get; set; } = [];

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public string? DateOfBirth { get; set; }

        [Required(ErrorMessage = "El periodo de vigencia es obligatorio.")]
        public string? ValidityPeriod { get; set; } = "2025-01-15 - 2025-01-31";

        [Required(ErrorMessage = "La contrasena es obligatoria.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "La contrasena debe tener minimo 8 caracteres.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirma la contrasena.")]
        [Compare(nameof(Password), ErrorMessage = "Las contrasenas no coinciden.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Selecciona un documento.")]
        public string? DocumentName { get; set; }

        public bool IsActive { get; set; } = true;

        [Range(typeof(bool), "true", "true", ErrorMessage = "Debes aceptar los terminos.")]
        public bool AcceptTerms { get; set; }
    }
}
