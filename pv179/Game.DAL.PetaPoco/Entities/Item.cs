using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.PetaPoco
{
    [TableName(TableNames.ItemTable)]
    public class Item : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Weight { get; set; }

        public int Price { get; set; }

        public bool Equipped { get; set; }

        public int? OwnerId { get; set; }
        [Ignore]
        public Character Owner { get; set; }

        public int? ShopOwnerId { get; set; }
        [Ignore]
        public Character ShopOwner { get; set; }

        public int WeaponTypeId { get; set; }
        [Ignore]
        public WeaponType WeaponType { get; set; }
    }
}
