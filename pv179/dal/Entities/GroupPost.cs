using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Game.Infrastructure;

namespace Game.DAL.Entity.Entities
{
    public class GroupPost : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid? CharacterId { get; set; }
        public virtual Character Author { get; set; }

        [MaxLength(1024)]
        public string Text { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        public DateTime Timestamp { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.GroupPosts);
    }
}
