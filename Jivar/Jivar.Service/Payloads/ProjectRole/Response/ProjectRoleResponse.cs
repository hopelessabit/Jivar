using Jivar.Service.Payloads.Account.Response;
using ProjectRoleModel = Jivar.BO.Models.ProjectRole;


namespace Jivar.Service.Payloads.ProjectRole.Response
{
    public class ProjectRoleResponse
    {
        public int accountId {  get; set; }
        public string accountName { get; set; }
        public string role {  get; set; }

        public ProjectRoleResponse(AccountInfoResponse? account, ProjectRoleModel role)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            this.accountId = account.Id;
            this.accountName = account.Name;
            this.role = role.ToString();
        }
    }
}
