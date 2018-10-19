using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.PetaPoco
{
    [TableName(TableNames.GroupPostTable)]
    public class GroupPost : IEntity
    {
        public int Id { get; set; }

        public int? CharacterId { get; set; }
        [Ignore]
        public Character Author { get; set; }

        public string Text { get; set; }

        public int GroupId { get; set; }
        [Ignore]
        public Group Group { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
