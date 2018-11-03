using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Common;

namespace BL.DTO.Filters
{
    public class CharacterFilterDto : FilterDtoBase
    {
        public string Name { get; set; }
        public int ScoreMin { get; set; } = 0;
        public int ScoreMax { get; set; } = int.MaxValue;
    }
}
