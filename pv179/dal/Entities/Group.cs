using Game.DAL.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entity.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        public virtual ICollection<Character> Members { get; set; }

        public virtual ICollection<GroupPost> Wall { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Groups);
    }
}
