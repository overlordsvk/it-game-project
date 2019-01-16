using BL.DTO;
using System.Collections.Generic;

namespace GameWebMVC.Models
{
    public class FightModel
    {
        public FightDto Fight { get; set; }
        public ICollection<(int, int, int, int, int, int, int)> Steps { get; set; }
    }
}