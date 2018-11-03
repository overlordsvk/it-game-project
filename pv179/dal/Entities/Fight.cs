using Game.DAL.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public virtual ICollection<Item> AttackerItems { get; set; }

        public virtual ICollection<Item> DefenderItems { get; set; }

        public bool AttackSuccess { get; set; }

        public DateTime Timestamp { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Fights);
    }
}
