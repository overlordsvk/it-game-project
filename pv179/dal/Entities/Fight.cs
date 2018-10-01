using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Fight
    {
        public int Id { get; set; }
        public Character Attacker { get; set; }
        public Character Defender { get; set; }
        public Item AttackerItem { get; set; }
        public Item DefenderItem { get; set; }
        public bool AttackSuccess { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
