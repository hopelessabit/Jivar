using Jivar.DAO.DAOs;
using Jivar.BO.Models;
using Jivar.Repository.Interface;
using Jivar.DAO.Interface;

namespace Jivar.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository() : base(AccountDAO.Instance)
        {
        }
    }
}
