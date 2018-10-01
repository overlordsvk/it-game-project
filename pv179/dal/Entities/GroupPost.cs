using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class GroupPost
    {
        public int Id { get; set; }
        public Character Author { get; set; }
        public string Text { get; set; }
        public Group Group { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
