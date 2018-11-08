using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    class CharacterOnlyDto : DtoBase
    {
        public string Name { get; set; }

        public int Money { get; set; }

        public int Health { get; set; }

        public int Score { get; set; }

        public int Strength { get; set; }

        public int Perception { get; set; }

        public int Endurance { get; set; }

        public int Charisma { get; set; }

        public int Intelligence { get; set; }

        public int Agility { get; set; }

        public int Luck { get; set; }
    }
}
