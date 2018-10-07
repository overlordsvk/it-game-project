using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Item : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
        public int? OwnerId { get; set; }
        public bool Equipped { get; set; }
        public virtual Character Owner { get; set; }
        public int WeaponTypeId { get; set; }
        public virtual WeaponType WeaponType { get; set; }
    }
}
