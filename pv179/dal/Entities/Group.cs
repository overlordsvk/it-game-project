using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Game.Infrastructure;

namespace Game.DAL.Entity.Entities
{
    public class Group : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        
        [MaxLength(256)]
        public string Name { get; set; }

        public string Picture { get; set; }

        public virtual ICollection<Character> Members { get; set; }

        public virtual ICollection<GroupPost> Wall { get; set; }

        [MaxLength(4096)]
        public string Description { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Groups);
    }
}
