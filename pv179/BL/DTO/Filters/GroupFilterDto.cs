using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Common;

namespace BL.DTO.Filters
{
    public class GroupFilterDto : FilterDtoBase
    {
        public string Name { get; set; }
        public int MinMembers { get; set; } = 0;
        public int MaxMembers { get; set; } = 50;
    }
}
