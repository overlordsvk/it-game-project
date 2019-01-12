using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class AccountCreateDto
    {
        [Required(ErrorMessage = "Meno nesmie byť prázdne")]
        [MinLength(4, ErrorMessage = "Meno musí mať aspoň 4 znaky")]
        [MaxLength(64, ErrorMessage = "Meno nesmie mať viac ako 64 znakov")]
        [DisplayName("Meno")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email nesmie byť prázdny")]
        [EmailAddress(ErrorMessage = "Neplatný formát")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Heslo nesmie byť prázdne")]
        [MinLength(8, ErrorMessage = "Heslo musí mať aspoň 8 znakov")]
        [MaxLength(64, ErrorMessage = "Heslo nesmie mať viac ako 64 znakov")]
        [DisplayName("Heslo")]
        public string Password { get; set; }
    }
}