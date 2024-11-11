using Jivar.BO.Models;
using Task = System.Threading.Tasks.Task;

namespace Jivar.Service.Interfaces
{
    public interface IAccountService
    {
        Task<bool> IsEmailExist(string email);
        Task<bool> AddAccount(Account account);
    }
}
