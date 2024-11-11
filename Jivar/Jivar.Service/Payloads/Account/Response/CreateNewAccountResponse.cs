using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jivar.Service.Payloads.Account.Response
{
    public class CreateNewAccountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }

        public DateTime? Birthday { get; set; }

        public string Gender { get; set; }

        public DateTime CreateTime { get; set; }

        public string Role { get; set; }
    }
}
