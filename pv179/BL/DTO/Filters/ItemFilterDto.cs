using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Common;

namespace BL.DTO.Filters
{
    public class ItemFilterDto : FilterDtoBase
    {
        public string Name { get; set; }
        public int? OwnerId { get; set; }
        public int? ShopOwnerId { get; set; }
        public int? WeaponTypeId { get; set; }

    }
}
