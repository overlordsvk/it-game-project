using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class WeaponTypeDto : DtoBase
    {
        [MaxLength(256)]
        public string ItemName { get; set; }

        [Range(0, 500)]
        public int MaxAttack { get; set; }
        [Range(0, 500)]
        public int MaxDefense { get; set; }
        [Range(0, 100)]
        public int MaxWeight { get; set; }
        [Range(0, 500)]
        public int MinAttack { get; set; }
        [Range(0, 500)]
        public int MinDefense { get; set; }
        [Range(0, 100)]
        public int MinWeight { get; set; }
    }
}
