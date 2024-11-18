using AccountModel = Jivar.BO.Models.Account;
namespace Jivar.Service.Payloads.Account.Response
{
    public class AccountInfoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }

        public AccountInfoResponse(AccountModel account)
        {
            Id = account.Id;
            Name = account.Name;
            Email = account.Email;
            Phone = account.Phone;
            Birthday = account.Birthday;
            Gender = account.Gender;
            Role = account.Role;
        }
    }
}
