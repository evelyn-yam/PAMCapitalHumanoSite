namespace Template.Services.IServices.Auth
{
    public interface IAuthService
    {
        Task SignInAsync(string email, string password, bool rememberMe);

        Task SignUpAsync(string email, string password);

        Task SignOutAsync();
    }
}
