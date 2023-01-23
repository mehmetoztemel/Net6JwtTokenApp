using Net6JwtTokenApp.Models;

namespace Net6JwtTokenApp.Services._02_User
{
    public interface IUserService
    {
        Task<CustomResponse> GetUsers();
    }
}
