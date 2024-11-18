using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jivar.Service.Payloads.Account.Request
{
    public class CreateNewAccountRequest
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }

        public DateTime? Birthday { get; set; }

        public string Gender { get; set; }
        public string? FeUrl { get; set; }
    }
}
