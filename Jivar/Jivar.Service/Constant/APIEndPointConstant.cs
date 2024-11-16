namespace Jivar.Service.Constant
{
    public class APIEndPointConstant
    {
        static APIEndPointConstant()
        {
        }
        public const string RootEndPoint = "/api";
        public const string ApiVersion = "/v1";
        public const string ApiEndpoint = RootEndPoint + ApiVersion;

        public static class Authentication
        {
            public const string AuthenticationEndpoint = ApiEndpoint + "/auth";
            public const string Login = AuthenticationEndpoint + "/login";
            public const string CreateAccount = AuthenticationEndpoint + "/create";
            public const string UpdatePassword = AuthenticationEndpoint + "/changepass";
        }

        public static class SprintE
        {
            public const string SprintEndpoint = ApiEndpoint + "/sprint";
            public const string CreateSprint = SprintEndpoint + "/{projectId}";
            public const string GetSprintById = SprintEndpoint + "/{id}";
            public const string UpdateSprint = SprintEndpoint + "/{id}";
        }

        public static class TaskE
        {
            public const string TaskEndpoint = ApiEndpoint + "/task";
            public const string CreateTask = TaskEndpoint + "/{sprintId}";
            public const string GetTaskById = TaskEndpoint + "/{id}";
            public const string UpdateTask = TaskEndpoint + "/{id}";
        }
    }
}
