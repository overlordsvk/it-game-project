using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class AccountCreateDto
    {
        [Required, StringLength(64, MinimumLength = 4, ErrorMessage = "Username musí mať 4 až 64 znakov")]
        public string Username { get; set; }

        [Required, EmailAddress(ErrorMessage = "Neplatný formát")]
        public string Email { get; set; }

        [Required, StringLength(64, MinimumLength = 8, ErrorMessage = "Heslo musí mať 8 až 64 znakov")]
        public string Password { get; set; }
    }
}
