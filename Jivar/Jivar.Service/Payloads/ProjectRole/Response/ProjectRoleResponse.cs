using Jivar.Service.Payloads.Account.Response;
using ProjectRoleModel = Jivar.BO.Models.ProjectRole;


namespace Jivar.Service.Payloads.ProjectRole.Response
{
    public class ProjectRoleResponse
    {
        public int AccountId {  get; set; }
        public string AccountName { get; set; }
        public string Role {  get; set; }

        public ProjectRoleResponse(AccountInfoResponse? account, ProjectRoleModel role)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            this.AccountId = account.Id;
            this.AccountName = account.Name;
            this.Role = role.Role.ToString();
        }
    }
}
