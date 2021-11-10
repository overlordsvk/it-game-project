using BL.DTO.Common;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class AccountDto : DtoBase
    {
        [Required, MinLength(4, ErrorMessage = "Meno musí mať aspoň 4 znaky"), MaxLength(64, ErrorMessage = "Meno nesmie mať viac ako 64 znakov")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Neplatný formát")]
        public string Email { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public string Roles { get; set; }

        public CharacterDto Character { get; set; }
    }
}