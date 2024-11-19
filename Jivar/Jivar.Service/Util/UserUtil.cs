using Jivar.BO;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jivar.Service.Util
{
    public class UserUtil
    {
        public static int GenerateRandomSixDigitNumber()
        {
            Random random = new Random();
            return random.Next(100000, 1000000); // Generates a number between 100000 and 999999
        }

        public static int GetAccountId(HttpContext httpContext)
        {
            if (httpContext == null || httpContext.User == null)
            {
                throw new Exception("Can not read user Id");
            }

            var nameIdentifierClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifierClaim == null)
            {
                throw new Exception("Can not read user Id");
            }
            int accountId;
            try
            {
                accountId = int.Parse(nameIdentifierClaim.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not read user Id");
            }
            return accountId;
        }

        public static string GetRoleName(HttpContext httpContext)
            {
                var roleClaim = httpContext.User.FindFirst(ClaimTypes.Role);
                return roleClaim?.Value;
            }
        
    }
}
