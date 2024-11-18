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

        public static class ProjectE
        {
            public const string GetProjectByUserId = ApiEndpoint + "/user";
        }

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
            public const string GetSprintById = SprintEndpoint + "/{id}";
            public const string UpdateSprint = SprintEndpoint + "/{id}";
        }

        public static class TaskE
        {
            public const string TaskEndpoint = ApiEndpoint + "/task";
            public const string GetTaskById = TaskEndpoint + "/{id}";
            public const string UpdateStatusTask = TaskEndpoint + "/update-status/{id}";
        }

        public static class SubTaskE
        {
            public const string SubTaskEndpoint = ApiEndpoint + "/subTask";
            public const string GetTaskById = SubTaskEndpoint + "/{id}";
            public const string GetTaskByIV2 = SubTaskEndpoint + "/taskId={taskId}";
            public const string UpdateStatusTask = SubTaskEndpoint + "/update-status/{id}";
        }

        public static class DocumentE
        {
            public const string DocumentEndpoint = ApiEndpoint + "/document";
            public const string UploadFile = DocumentEndpoint + "/uploadFile";
            public const string GetDocumentById = DocumentEndpoint + "/{id}";
        }
    }
}
