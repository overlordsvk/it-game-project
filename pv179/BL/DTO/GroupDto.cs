using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class GroupDto
    {
        public string Name { get; set; }

        public virtual ICollection<Character> Members { get; set; }

        public virtual ICollection<GroupPost> Wall { get; set; }

        public string Description { get; set; }
    }
}
