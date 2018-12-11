using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameWebMVC.Models
{
    public class AccountLoginModel
    {
        [Required(ErrorMessage = "Meno nesmie byť prázdne")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Login musí mať 4 - 64 znakov")]
        [DisplayName("Meno/Email")]
        public string usernameOrEmail { get; set; }
        [Required(ErrorMessage = "Heslo nesmie byť prázdne")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Heslo musí mať 4 - 64 znakov")]
        [DisplayName("Heslo")]
        public string password { get; set; }

    }
}