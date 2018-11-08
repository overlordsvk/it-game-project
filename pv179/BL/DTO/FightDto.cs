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
        public Guid? AttackerId { get; set; }

        public Guid? DefenderId { get; set; }

        public Guid? AttackerItemId { get; set; }

        public Guid? DefenderItemId { get; set; }

        public bool AttackSuccess { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
