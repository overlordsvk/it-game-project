using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Common;

namespace BL.DTO.Filters
{
    public class GroupPostFilterDto : FilterDtoBase
    {
        public Guid? GroupId { get; set; }
    }
}
