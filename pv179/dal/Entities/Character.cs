using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Character : IEntity
    {
        [Key, ForeignKey("Account")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int Money { get; set; }
        public int Health { get; set; }
        public int Score { get; set; }
        public int Strength { get; set; }
        public int Perception { get; set; }
        public int Endurance { get; set; }
        public int Charisma { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int Luck { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Item> Shop { get; set; }
        //[ForeignKey(nameof(Equiped))]
        //public int? ItemId { get; set; }
        //public Item Equiped { get; set; }
        public virtual Group Group { get; set; }

        public virtual Account Account { get; set; }
    }
}
