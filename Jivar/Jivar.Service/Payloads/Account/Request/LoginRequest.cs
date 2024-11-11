using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jivar.Service.Payloads.Account.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Email's max length is 100 characters")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(64, ErrorMessage = "Password's max length is 64 characters")]
        public string Password { get; set; }
    }
}
