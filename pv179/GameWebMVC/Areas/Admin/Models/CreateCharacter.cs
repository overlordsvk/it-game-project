using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameWebMVC.Areas.Admin.Models
{
    public class CreateCharacter
    {
        public CharacterDto Character { get; set; }
        public Guid AccountId { get; set; }
    }
}