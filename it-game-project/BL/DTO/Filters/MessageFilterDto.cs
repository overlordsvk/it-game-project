using BL.DTO.Common;
using System;

namespace BL.DTO.Filters
{
    public class MessageFilterDto : FilterDtoBase
    {
        public Guid? ChatId { get; set; }
    }
}