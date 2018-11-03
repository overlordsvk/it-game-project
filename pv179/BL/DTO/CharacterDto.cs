using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class CharacterDto : DtoBase
    {
        [Required, MaxLength(64)]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Money { get; set; }

        [Range(0, 1000)]
        public int Health { get; set; }

        [Range(0, int.MaxValue)]
        public int Score { get; set; }

        [Range(0, 10)]
        public int Strength { get; set; }

        [Range(0, 10)]
        public int Perception { get; set; }

        [Range(0, 10)]
        public int Endurance { get; set; }

        [Range(0, 10)]
        public int Charisma { get; set; }

        [Range(0, 10)]
        public int Intelligence { get; set; }

        [Range(0, 10)]
        public int Agility { get; set; }

        [Range(0, 10)]
        public int Luck { get; set; }

        public GroupDto Group { get; set; }

        public AccountDto Account { get; set; }


    }
}
