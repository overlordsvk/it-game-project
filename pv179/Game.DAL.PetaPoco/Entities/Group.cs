using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.PetaPoco
{
    [TableName(TableNames.GroupTable)]
    public class Group : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [Ignore]
        public ICollection<Character> Members { get; set; }
        [Ignore]
        public ICollection<GroupPost> Wall { get; set; }
    }
}
