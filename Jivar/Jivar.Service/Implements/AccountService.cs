using Jivar.BO.Models;
using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Jivar.Service.Implements
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> IsEmailExist(string email)
        {
            var account = await _accountRepository.GetAsync(ft => ft.Email == email);
            return account != null;
        }


        public async Task<bool> AddAccount(Account account)
        {

            return await _accountRepository.AddAsync(account);
        }
    }
}
