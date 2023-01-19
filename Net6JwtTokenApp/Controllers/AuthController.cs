using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net6JwtTokenApp.Models;
using Net6JwtTokenApp.Services;

namespace Net6JwtTokenApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        ITokenService _tokenService;
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Login(TokenRequest tokenRequest)
        {
            return CustomResult(_tokenService.CreateToken(tokenRequest));
        }

        [Route("refreshtoken")]
        [HttpGet]
        public IActionResult RefreshToken([FromForm] string refreshToken)
        {

            return CustomResult(_tokenService.RefreshTokenUpdate(refreshToken));

        }
    }
}
