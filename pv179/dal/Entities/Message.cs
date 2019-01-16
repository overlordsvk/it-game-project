using Game.DAL.Entity;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game.DAL.Entities
{
    public class Message : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        [MaxLength(4096)]
        public string Text { get; set; }

        public Guid? AuthorId { get; set; }
        public virtual Character Author { get; set; }

        public DateTime Timestamp { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Messages);
    }
}