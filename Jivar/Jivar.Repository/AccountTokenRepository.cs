using Jivar.DAO.DAOs;
using Jivar.BO.Models;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class AccountTokenRepository : Repository<AccountToken>, IAccountTokenRepository
    {
        public AccountTokenRepository() : base(AccountTokenDAO.Instance)
        {
        }
    }
}
