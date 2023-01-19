using Microsoft.IdentityModel.Tokens;
using Net6JwtTokenApp.Context;
using Net6JwtTokenApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Net6JwtTokenApp.Services
{
    public class TokenService : ITokenService
    {
        private RefreshTokenDbContext _context;
        public TokenService(RefreshTokenDbContext context)
        {
           _context= context;   
        }
        public CustomResponse CreateToken(TokenRequest tokenRequest)
        {

            var user = _context.Users.FirstOrDefault(x => x.Username == tokenRequest.Username && x.Password == tokenRequest.Password);


            if (user != null)
            {
                TokenResponse tokenResponse = new TokenResponse();
                //Security Key'in simetriğini tanımlıyoruz.
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("net6jwttokenproject"));

                //Şifrelenmiş kimliği oluşturuyoruz.
                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                //Oluşturulacak token ayarlarını veriyoruz.
                tokenResponse.Expiration = DateTime.Now.AddMinutes(2);
                var claims = new List<Claim> {
                new Claim("User", tokenRequest.Username),
                //new Claim(ClaimTypes.Name , loginModel.UserName)
                };

                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    expires: tokenResponse.Expiration, //Token süresini 1 gün olarak belirliyorum
                    notBefore: DateTime.Now, //Token üretildikten ne kadar süre sonra devreye girsin ayarlıyouz.
                    signingCredentials: signingCredentials, // Şifrelenmiş kimliği buraya yazıyoruz
                    claims: claims //Token içinde tutulacak kullanici bilgilerini dolduruyoruz
                    );

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                tokenResponse.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

                tokenResponse.RefreshToken = CreateRefreshToken();

                user.RefreshToken = tokenResponse.RefreshToken;
                user.RefreshTokenEndDate = tokenResponse.Expiration.AddMinutes(3);
                _context.SaveChanges();

                return CustomResponse.Success(200, tokenResponse);
            }
            else
            {
                return CustomResponse.Error(401, "Kullanıcı bilgileri hatalı");
            }

        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        public CustomResponse RefreshTokenUpdate(string refreshToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);

            if (user != null && user.RefreshTokenEndDate > DateTime.Now) {

                var tokenRequest = new TokenRequest
                {
                    Username = user.Username,
                    Password = user.Password
                };
                return CreateToken(tokenRequest);
            }
            return CustomResponse.Error(401,"Token süresi dolmuştur.");


        }
    }
}
