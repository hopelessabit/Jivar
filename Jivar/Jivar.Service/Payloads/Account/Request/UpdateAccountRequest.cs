namespace Jivar.Service.Payloads.Account.Request
{
    public class UpdateAccountRequest
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }
    }
}
