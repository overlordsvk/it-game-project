using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.PetaPoco.Entities
{
    [TableName(TableNames.FightTable)]
    
    public class Fight : IEntity
    {
        public int Id { get; set; }

        public int? AttackerId { get; set; }
        [Ignore]
        public Character Attacker { get; set; }

        public int? DefenderId { get; set; }
        [Ignore]
        public Character Defender { get; set; }

        public int? AttackerItemId { get; set; }
        [Ignore]
        public Item AttackerItem { get; set; }

        public int? DefenderItemId { get; set; }
        [Ignore]
        public Item DefenderItem { get; set; }

        public bool AttackSuccess { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
