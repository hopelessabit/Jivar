using Jivar.BO;
using Jivar.BO.Models;
using Jivar.DAO.Interface;

namespace Jivar.DAO.DAOs
{
    public class AccountDAO : BaseDAO<Account>, IAccountDAO
    {
        private static AccountDAO? _instance;
        public static AccountDAO Instance => _instance ??= new AccountDAO(new JivarDbContext());
        public AccountDAO(JivarDbContext context) : base(context)
        {
        }

    }
}
