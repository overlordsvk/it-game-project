using Castle.Core.Smtp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameWebMVC.Models
{
    public class ChatCreate
    {
        [Required(ErrorMessage = "Prijímateľ nesmie byť prázdny")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Prijímateľ musí mať 4 - 64 znakov")]
        [DisplayName("Prijímateľ")]
        public String ReceiverName { get; set; }
        [Required(ErrorMessage = "Predmet nesmie byť prázdny")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Predmet musí mať 4 - 64 znakov")]
        [DisplayName("Predmet")]
        public String Subject { get; set; }
    }
}