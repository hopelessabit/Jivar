using Jivar.BO;
using Jivar.BO.Models;
using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Jivar.Service.Util;


namespace Jivar.Service.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IAccountRepository accountRepository, IOptions<JwtSettings> jwtSettings)
        {
            _accountRepository = accountRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<(Account, int?)> Authenticate(string email, string password)
        {
            Account? account = await _accountRepository.GetAsync(ft => ft.Email == email);
            if (account == null || !VerifyPassword(account.Password, password))
            {
                return (null, null);
            }
            
            return (account, account.Id);
        }

        private bool VerifyPassword(string storedPassword, string providedPassword)
        {
            return PasswordUtil.VerifyPassword(providedPassword, storedPassword); 
        }

        public async Task<string> GenerateJwtToken(Account account, int? actorId)
        {
            if (string.IsNullOrEmpty(account.Role))
            {
                throw new ArgumentException("Role name cannot be null or empty.");
            }

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new Claim(ClaimTypes.Email, account.Email),
            new Claim(ClaimTypes.Role, account.Role)
        };
            if (actorId.HasValue)
            {
                claims.Add(new Claim("actorId", actorId.Value.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.Lifetime);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}

