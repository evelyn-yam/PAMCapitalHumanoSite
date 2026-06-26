namespace Template.Services.IServices.Auth
{
    public interface IAccessTokenService
    {
        Task<string?> GetTokenAsync();

        Task SetTokenAsync(string token, bool rememberMe = true);

        Task ClearTokenAsync();
    }
}
