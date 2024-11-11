using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
