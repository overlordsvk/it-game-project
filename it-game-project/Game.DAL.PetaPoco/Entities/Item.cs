using AsyncPoco;
using Game.DAL.PetaPoco.Enums;
using Game.Infrastructure;
using System;

namespace Game.DAL.PetaPoco.Entities
{
    [TableName(TableNames.ItemTable)]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Item : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Weight { get; set; }

        public int Price { get; set; }

        public bool Equipped { get; set; }

        public Guid? OwnerId { get; set; }

        [Ignore]
        public Character Owner { get; set; }

        public ItemType ItemType { get; set; }

        [Ignore]
        public string TableName { get; } = TableNames.ItemTable;
    }
}