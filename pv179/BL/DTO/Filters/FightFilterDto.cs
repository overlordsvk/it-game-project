using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Common;

namespace BL.DTO.Filters
{
    public class FightFilterDto : FilterDtoBase
    {
        public Guid? AttackerId { get; set; }

        public Guid? DefenderId { get; set; }

        public Guid? FighterId { get; set; }

        public bool? AttackSuccess { get; set; }

    }
}
