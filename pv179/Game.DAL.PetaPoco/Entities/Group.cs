using AsyncPoco;
using Game.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.PetaPoco.Entities
{
    [TableName(TableNames.GroupTable)]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Group : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        [Ignore]
        public virtual ICollection<Character> Members { get; set; }

        [Ignore]
        public virtual ICollection<GroupPost> Wall { get; set; }

        public string Description { get; set; }

        [Ignore]
        public string TableName { get; } = TableNames.GroupTable;
    }
}
