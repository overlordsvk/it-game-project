using BL.DTO.Common;
using System;

namespace BL.DTO.Filters
{
    public class CharacterFilterDto : FilterDtoBase
    {
        public string Name { get; set; }
        public Guid? GroupId { get; set; }
        public int ScoreMin { get; set; } = 0;
        public int ScoreMax { get; set; } = int.MaxValue;
    }
}