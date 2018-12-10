using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.DAL.Enums;
using Newtonsoft.Json;
using System.ComponentModel;

namespace BL.DTO
{
    public class ItemDto : DtoBase
    {
        [MaxLength(256)]
        [DisplayName("Názov")]
        public string Name { get; set; }

        [Range(1, 500)]
        [DisplayName("Útok")]
        public int Attack { get; set; }

        [Range(1, 500)]
        [DisplayName("Obrana")]
        public int Defense { get; set; }

        [Range(1, 10)]
        [DisplayName("Váha")]
        public int Weight { get; set; }

        [Range(0, int.MaxValue)]
        [DisplayName("Cena")]
        public int Price { get; set; }

        [DisplayName("Vyzbrojený")]
        public bool Equipped { get; set; }

        public Guid? OwnerId { get; set; }
        [JsonIgnore]
        public CharacterDto Owner { get; set; }

        [DisplayName("Typ")]
        public ItemType ItemType { get; set; }
    }
}
