using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Character> Members { get; set; }
        public virtual ICollection<GroupPost> Wall { get; set; }
    }
}
