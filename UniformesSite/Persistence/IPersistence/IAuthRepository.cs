using Template.Dtos.Auth;

namespace Template.Persistence.IPersistence
{
    public interface IAuthRepository
    {
        Task<SignInResponseDto> SignInAsync(SignInRequestDto request);

        Task<SignInResponseDto> SignUpAsync(SignUpRequestDto request);
    }
}
