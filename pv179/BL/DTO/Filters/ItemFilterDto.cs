using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Common;
using Game.DAL.Enums;

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
