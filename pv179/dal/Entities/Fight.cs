using Game.DAL.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entity.Entities
{
    public class Fight : IEntity
    {
        public int Id { get; set; }

        public int? AttackerId { get; set; }
        public virtual Character Attacker { get; set; }

        public int? DefenderId { get; set; }
        public virtual Character Defender { get; set; }

        public int? AttackerItemId { get; set; }
        public virtual Item AttackerItem { get; set; }

        public int? DefenderItemId { get; set; }
        public virtual Item DefenderItem { get; set; }

        public bool AttackSuccess { get; set; }

        public DateTime Timestamp { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Fights);
    }
}
