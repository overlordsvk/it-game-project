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
        [Required, MinLength(4, ErrorMessage = "Meno musí mať aspoň 4 znaky"), MaxLength(64, ErrorMessage = "Meno nesmie mať viac ako 64 znakov")]
        public string Username { get; set; }

        [Required, EmailAddress(ErrorMessage = "Neplatný formát")]
        public string Email { get; set; }

        [Required, MinLength(8, ErrorMessage = "Heslo musí mať aspoň 8 znakov"), MaxLength(64, ErrorMessage = "Heslo nesmie mať viac ako 64 znakov")]
        public string Password { get; set; }
    }
}
