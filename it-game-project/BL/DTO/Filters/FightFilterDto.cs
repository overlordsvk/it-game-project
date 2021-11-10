using BL.DTO.Common;
using System;

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