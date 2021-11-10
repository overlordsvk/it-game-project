using BL.DTO.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class CharacterDto : DtoBase
    {
        [Required(ErrorMessage = "Meno nesmie byť prázdne")]
        [MinLength(4, ErrorMessage = "Meno musí mať aspoň 4 znaky")]
        [MaxLength(64, ErrorMessage = "Meno nesmie mať viac ako 64 znakov")]
        [DisplayName("Meno")]
        public string Name { get; set; }

        private int money = 100;

        [Range(0, int.MaxValue)]
        [DisplayName("Peniaze")]
        public int Money
        {
            get { return money; }
            set { money = value < 0 || value > int.MaxValue ? value < 0 ? 0 : int.MaxValue : value; }
        }

        [Range(0, 1000)]
        [DisplayName("Zdravie")]
        public int Health { get; set; } = 0;

        private int score = 0;

        [Range(0, int.MaxValue)]
        [DisplayName("Skóre")]
        public int Score
        {
            get { return score; }
            set { score = value < 0 || value > int.MaxValue ? value < 0 ? 0 : int.MaxValue : value; }
        }

        [Range(1, 10, ErrorMessage = "Rozsah medzi 1-10")]
        [DisplayName("Sila")]
        public int Strength { get; set; }

        [Range(1, 10, ErrorMessage = "Rozsah medzi 1-10")]
        [DisplayName("Reflexy")]
        public int Perception { get; set; }

        [Range(1, 10, ErrorMessage = "Rozsah medzi 1-10")]
        [DisplayName("Výdrž")]
        public int Endurance { get; set; }

        [Range(1, 10, ErrorMessage = "Rozsah medzi 1-10")]
        [DisplayName("Charizma")]
        public int Charisma { get; set; }

        [Range(1, 10, ErrorMessage = "Rozsah medzi 1-10")]
        [DisplayName("Inteligencia")]
        public int Intelligence { get; set; }

        [Range(1, 10, ErrorMessage = "Rozsah medzi 1-10")]
        [DisplayName("Obratnosť")]
        public int Agility { get; set; }

        [Range(1, 10, ErrorMessage = "Rozsah medzi 1-10")]
        [DisplayName("Štastie")]
        public int Luck { get; set; }

        [JsonIgnore]
        public ICollection<ItemDto> Items { get; set; } = new List<ItemDto>();

        [JsonIgnore]
        public ICollection<ChatDto> SenderChats { get; set; } = new List<ChatDto>();

        [JsonIgnore]
        public ICollection<ChatDto> ReceiverChats { get; set; } = new List<ChatDto>();

        [JsonIgnore]
        public ICollection<FightDto> AttackerFights { get; set; } = new List<FightDto>();

        [JsonIgnore]
        public ICollection<FightDto> DefenderFights { get; set; } = new List<FightDto>();

        [DisplayName("Názov skupiny")]
        public Guid? GroupId { get; set; }

        [JsonIgnore]
        public GroupDto Group { get; set; }

        public bool IsGroupAdmin { get; set; }
    }
}