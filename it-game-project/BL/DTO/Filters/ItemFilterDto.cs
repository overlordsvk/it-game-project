using BL.DTO.Common;
using Game.DAL.Enums;
using System;

namespace BL.DTO.Filters
{
    public class ItemFilterDto : FilterDtoBase
    {
        public string Name { get; set; }
        public Guid? OwnerId { get; set; }
        public ItemType? ItemType { get; set; }
        public bool? IsEquipped { get; set; }
    }
}