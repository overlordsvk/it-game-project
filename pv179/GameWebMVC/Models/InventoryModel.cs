using BL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GameWebMVC.Models
{
    public class InventoryModel
    {
        [DisplayName("Peniaze")]
        public int Money { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
    }
}