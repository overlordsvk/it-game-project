using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.DAL.Entity;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;

namespace dal.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }

        [MaxLength(2048)]
        public string Text { get; set; }

        public int AuthorId { get; set; }
        public virtual Character Author { get; set; }

        public DateTime Timestamp { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Messages);
    }
}
