using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Common;

namespace BL.DTO.Filters
{
    public class ChatFilterDto : FilterDtoBase
    {
        public Guid? CharacterId { get; set; }
    }
}
