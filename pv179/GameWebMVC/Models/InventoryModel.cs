using BL.DTO;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameWebMVC.Models
{
    public class InventoryModel
    {
        [DisplayName("Peniaze")]
        public int Money { get; set; }

        public IEnumerable<ItemDto> Items { get; set; }
    }
}