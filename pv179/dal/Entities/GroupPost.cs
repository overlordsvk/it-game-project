using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class GroupPost : IEntity
    {
        public int Id { get; set; }
        public int? CharacterId { get; set; }
        public virtual Character Author { get; set; }
        public string Text { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
