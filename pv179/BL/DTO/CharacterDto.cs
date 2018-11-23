using BL.DTO.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace BL.DTO
{
    public class CharacterDto : DtoBase
    {
        [Required, StringLength(64, MinimumLength = 4, ErrorMessage = "Meno musí mať aspoň 4 znaky")]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Money { get; set; } = 100;

        [Range(0, 1000, ErrorMessage = "0-10")]
        public int Health { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "0-10")]
        public int Score { get; set; } = 0;

        [Range(0, 10, ErrorMessage = "0-10")]
        public int Strength { get; set; }

        [Range(0, 10, ErrorMessage = "0-10")]
        public int Perception { get; set; }

        [Range(0, 10, ErrorMessage = "0-10")]
        public int Endurance { get; set; }

        [Range(0, 10, ErrorMessage = "0-10")]
        public int Charisma { get; set; }

        [Range(0, 10, ErrorMessage = "0-10")]
        public int Intelligence { get; set; }

        [Range(0, 10, ErrorMessage = "0-10")]
        public int Agility { get; set; }

        [Range(0, 10, ErrorMessage = "0-10")]
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
        
        public Guid? GroupId { get; set; }
        [JsonIgnore]
        public GroupDto Group { get; set; }

        public bool IsGroupAdmin { get; set; }
    }
}