﻿using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class ItemDto : DtoBase
    {
        [MaxLength(256)]
        public string Name { get; set; }

        [Range(0, 500)]
        public int Attack { get; set; }

        [Range(0, 500)]
        public int Defense { get; set; }

        [Range(0, 100)]
        public int Weight { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        public bool Equipped { get; set; }

        public int? OwnerId { get; set; }
        public CharacterDto Owner { get; set; }

    }
}
