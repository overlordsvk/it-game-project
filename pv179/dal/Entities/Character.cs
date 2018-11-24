using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Game.Infrastructure;

namespace Game.DAL.Entity.Entities
{
    public class Character : IEntity
    {
        [Key, ForeignKey("Account"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required, MaxLength(64), MinLength(4)]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Money { get; set; }

        [Range(0,1000)]
        public int Health { get; set; }

        [Range(0, int.MaxValue)]
        public int Score { get; set; }

        [Range(1,10)]
        public int Strength { get; set; }

        [Range(1,10)]
        public int Perception { get; set; }

        [Range(1,10)]
        public int Endurance { get; set; }

        [Range(1,10)]
        public int Charisma { get; set; }

        [Range(1,10)]
        public int Intelligence { get; set; }

        [Range(1,10)]
        public int Agility { get; set; }

        [Range(1,10)]
        public int Luck { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public virtual ICollection<Chat> SenderChats { get; set; }
        public virtual ICollection<Chat> ReceiverChats { get; set; }

        public virtual ICollection<Fight> AttackerFights { get; set; }
        public virtual ICollection<Fight> DefenderFights { get; set; }

        public Guid? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public virtual Account Account { get; set; }

        public bool IsGroupAdmin { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Characters);
    }
}
