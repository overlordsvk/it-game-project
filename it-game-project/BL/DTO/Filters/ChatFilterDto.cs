using BL.DTO.Common;
using System;

namespace BL.DTO.Filters
{
    public class ChatFilterDto : FilterDtoBase
    {
        public Guid? CharacterId { get; set; }
    }
}