using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class AccountDto : DtoBase
    {
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Username musí mať 4 až 64 znakov")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Neplatný formát")]
        public string Email { get; set; }

        public string PasswordSalt { get; set; } 

        public string PasswordHash { get; set; }

        public string Roles { get; set; }

        public CharacterDto Character { get; set; }

    }
}
