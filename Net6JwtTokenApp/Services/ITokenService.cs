using Net6JwtTokenApp.Models;

namespace Net6JwtTokenApp.Services
{
    public interface ITokenService
    {
        CustomResponse CreateToken(TokenRequest tokenRequest);
        string CreateRefreshToken();

        CustomResponse RefreshTokenUpdate(string refreshToken);

    }
}
