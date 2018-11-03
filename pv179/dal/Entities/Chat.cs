using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Game.Infrastructure;

namespace Game.DAL.Entity.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Sender))]
        public int? SenderId { get; set; }
        public virtual Character Sender { get; set; }

        [ForeignKey(nameof(Receiver))]
        public int? ReceiverId { get; set; }
        public virtual Character Receiver { get; set; }

        [MaxLength(256)]
        public string Subject { get; set; }

        public virtual ICollection<Chat> Messages { get; set; }
        
        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Chat);
    }
}
