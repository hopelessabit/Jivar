using Jivar.BO.Models;
using Task = System.Threading.Tasks.Task;

namespace Jivar.Service.Interfaces
{
    public interface IAccountService
    {
        Task<bool> IsEmailExist(string email);
        Task<bool> AddAccount(Account account);
        Task<List<Account>> GetAccountsByIds(List<int> ids);
        Task<Account> GetAccountById(int id);
    }
}
