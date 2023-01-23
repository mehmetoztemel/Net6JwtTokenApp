using Microsoft.EntityFrameworkCore;
using Net6JwtTokenApp.Context;
using Net6JwtTokenApp.Models;

namespace Net6JwtTokenApp.Services._02_User
{
    public class UserService : IUserService
    {
        private RefreshTokenDbContext _context;

        public UserService(RefreshTokenDbContext context)
        {
            _context = context;
        }

        public async Task<CustomResponse> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return CustomResponse.Success(200, users);
        }
    }
}
