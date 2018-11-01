using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class FightDto : DtoBase
    {
        public int? AttackerId { get; set; }

        public int? DefenderId { get; set; }

        public int? AttackerItemId { get; set; }

        public int? DefenderItemId { get; set; }

        public bool AttackSuccess { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
