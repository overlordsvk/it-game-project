using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public ICollection<ItemDto> Items { get; set; } = new List<ItemDto>();

        public ICollection<ChatDto> SenderChats { get; set; } = new List<ChatDto>();
        public ICollection<ChatDto> ReceiverChats { get; set; } = new List<ChatDto>();

        public ICollection<FightDto> AttackerFights { get; set; } = new List<FightDto>();
        public ICollection<FightDto> DefenderFights { get; set; } = new List<FightDto>();

        public Guid? GroupId { get; set; }
        public GroupDto Group { get; set; }

        public bool IsGroupAdmin { get; set; }
    }
}