using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorWebRtc_Application.Features.Commands.Account.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, (bool,string)>
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public LoginHandler(AppDbContext context,IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context; 
        }

        public async Task<(bool,string)> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken);

            if (user == null || !VerifyPassword(request.Password,user.PasswordHash,user.PasswordSalt)) {

                return (false,string.Empty);
            }

            var token = GenerateJwtToken(user);

            return (true,token);
        }


        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var SecretKey = jwtSettings.GetValue<string>("SecretKey");
            var Issuer = jwtSettings.GetValue<string>("Issuer");
            var Audience = jwtSettings.GetValue<string>("Audience");
            var ExpirationMinutes = jwtSettings.GetValue<int>("ExpirationMinutes");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            };
            var token = new JwtSecurityToken(
                Issuer, Audience, claims, expires: DateTime.Now.AddMinutes(ExpirationMinutes), signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token); 

        }


        private bool VerifyPassword(string password,string storedHash,string storedSalt)
        {
          
            byte[] salt = Convert.FromBase64String(storedSalt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));

            return hashed == storedHash;

        }




    }
}
