using Jivar.BO.Models;
using Jivar.Service.Payloads.Account.Request;
using Jivar.Service.Payloads.Account.Response;
using Task = System.Threading.Tasks.Task;

namespace Jivar.Service.Interfaces
{
    public interface IAccountService
    {
        Task<bool> IsEmailExist(string email);
        Task<bool> AddAccount(Account account);
        Task<List<Account>> GetAccountsByIds(List<int> ids);
        Task<Account> GetAccountById(int id);
        Task<AccountInfoResponse> UpdateAccountInfo(int id, UpdateAccountRequest request);
        Task<AccountInfoResponse> GetInfoById(int id);
    }
}
