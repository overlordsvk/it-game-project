using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Game.Infrastructure;

namespace Game.DAL.Entity.Entities
{
    public class Fight : IEntity
    {
        public int Id { get; set; }

        public int? AttackerId { get; set; }
        public virtual Character Attacker { get; set; }

        public int? DefenderId { get; set; }
        public virtual Character Defender { get; set; }

        public int? AttackerWeaponId { get; set; }
        public virtual Item AttackerWeapon { get; set; }

        public int? AttackerArmorId { get; set; }
        public virtual Item AttackerArmor { get; set; }

        public int? DefenderWeaponId { get; set; }
        public virtual Item DefenderWeapon { get; set; }

        public int? DefenderArmorId { get; set; }
        public virtual Item DefenderArmor { get; set; }

        public bool AttackSuccess { get; set; }

        public DateTime Timestamp { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Fights);
    }
}
