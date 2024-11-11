using Jivar.BO.Models;

namespace Jivar.DAO.DAOs
{
    public class AccountTokenDAO : BaseDAO<AccountToken>
    {
        private static AccountTokenDAO? _instance;
        public static AccountTokenDAO Instance => _instance ??= new AccountTokenDAO(new JivarDbContext());
        public AccountTokenDAO(JivarDbContext context) : base(context)
        {
        }
    }
}
