namespace Template.Modules.Features
{
    /// <summary>
    /// Rutas de autenticacion configurables por entorno.
    /// </summary>
    public sealed class AuthOptions
    {
        public const string SectionName = "Auth";

        public string SignInPath { get; set; } = "./v1/auth/sign-in";

        public string SignUpPath { get; set; } = "./v1/auth/sign-up";
    }
}
