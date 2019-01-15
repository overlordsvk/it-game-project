using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameWebMVC.Models
{
    public class FightModel
    {
        public FightDto Fight { get; set; }
        public ICollection<(int, int, int, int, int, int, int)> Steps  { get; set; }
           
    }
}