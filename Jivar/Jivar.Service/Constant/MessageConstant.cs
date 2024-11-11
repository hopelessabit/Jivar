using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jivar.Service.Constant
{
    public static class MessageConstant
    {
        public static class LoginMessage
        {
            public const string InvalidUsernameOrPassword = "Tên" +
                "" +
                "n đăng nhập hoặc mật khẩu không chính xác";
            public const string DeactivatedAccount = "Tài khoản đang bị vô hiệu hoá";
            public const string NotFoundAccount = "Không tìm thấy account";
        }

        public static class AccountMessage
        {
            public const string EmailExist = "Email đã tồn tại trong hệ thống";
        }
    }
}
