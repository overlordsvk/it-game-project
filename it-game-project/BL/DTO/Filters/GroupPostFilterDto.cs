using BL.DTO.Common;
using System;

namespace BL.DTO.Filters
{
    public class GroupPostFilterDto : FilterDtoBase
    {
        public Guid? GroupId { get; set; }
    }
}