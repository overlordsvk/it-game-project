using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Game.Infrastructure;

namespace Game.DAL.Entity.Entities
{
    public class Fight : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid? AttackerId { get; set; }
        public virtual Character Attacker { get; set; }

        public Guid? DefenderId { get; set; }
        public virtual Character Defender { get; set; }

        public Guid? AttackerWeaponId { get; set; }
        public virtual Item AttackerWeapon { get; set; }

        public Guid? AttackerArmorId { get; set; }
        public virtual Item AttackerArmor { get; set; }

        public Guid? DefenderWeaponId { get; set; }
        public virtual Item DefenderWeapon { get; set; }

        public Guid? DefenderArmorId { get; set; }
        public virtual Item DefenderArmor { get; set; }

        public bool AttackSuccess { get; set; }

        public DateTime Timestamp { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Fights);
    }
}
