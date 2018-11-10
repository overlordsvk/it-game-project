using AsyncPoco;
using Game.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.PetaPoco.Entities
{
    [TableName(TableNames.MessageTable)]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Message : IEntity
    {
        public Guid Id { get; set; }

        public Guid ChatId { get; set; }
        [Ignore]
        public Chat Chat { get; set; }

        public string Text { get; set; }

        public Guid? AuthorId { get; set; }
        [Ignore]
        public Character Author { get; set; }

        public DateTime Timestamp { get; set; }

        [Ignore]
        public string TableName { get; } = TableNames.MessageTable;
    }
}
