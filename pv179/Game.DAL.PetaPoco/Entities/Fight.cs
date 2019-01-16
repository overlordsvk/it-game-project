using AsyncPoco;
using Game.Infrastructure;
using System;

namespace Game.DAL.PetaPoco.Entities
{
    [TableName(TableNames.FightTable)]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Fight : IEntity
    {
        public Guid Id { get; set; }

        public Guid? AttackerId { get; set; }

        [Ignore]
        public Character Attacker { get; set; }

        public Guid? DefenderId { get; set; }

        [Ignore]
        public Character Defender { get; set; }

        public Guid? AttackerWeaponId { get; set; }

        [Ignore]
        public Item AttackerWeapon { get; set; }

        public Guid? AttackerArmorId { get; set; }

        [Ignore]
        public Item AttackerArmor { get; set; }

        public Guid? DefenderWeaponId { get; set; }

        [Ignore]
        public Item DefenderWeapon { get; set; }

        public Guid? DefenderArmorId { get; set; }

        [Ignore]
        public Item DefenderArmor { get; set; }

        public bool AttackSuccess { get; set; }

        public DateTime Timestamp { get; set; }

        [Ignore]
        public string TableName { get; } = TableNames.FightTable;
    }
}