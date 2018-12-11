using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameWebMVC.Models
{
    public class InventoryModel
    {
        public int Money { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
    }
}