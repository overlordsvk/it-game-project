using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Common;

namespace BL.DTO.Filters
{
    public class MessageFilterDto : FilterDtoBase
    {
        public int? ChatId { get; set; }

    }
}
