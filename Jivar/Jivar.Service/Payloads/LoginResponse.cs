namespace Jivar.Service.PayLoads
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public int actorId { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
