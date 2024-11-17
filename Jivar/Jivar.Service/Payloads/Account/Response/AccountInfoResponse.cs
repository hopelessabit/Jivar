using AccountModel = Jivar.BO.Models.Account;
namespace Jivar.Service.Payloads.Account.Response
{
    public class AccountInfoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AccountInfoResponse(AccountModel account)
        {
            Id = account.Id;
            Name = account.Name;
        }
    }
}
