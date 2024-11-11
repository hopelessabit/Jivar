using Jivar.BO.Models;

namespace Jivar.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(Account account, int? actorId);
        Task<(Account, int?)> Authenticate(string email, string password);
    }
}
