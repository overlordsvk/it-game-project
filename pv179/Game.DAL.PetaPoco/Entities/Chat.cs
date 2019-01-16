using AsyncPoco;
using Game.Infrastructure;
using System;
using System.Collections.Generic;

namespace Game.DAL.PetaPoco.Entities
{
    [TableName(TableNames.ChatTable)]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Chat : IEntity
    {
        public Guid Id { get; set; }

        public Guid? SenderId { get; set; }

        [Ignore]
        public Character Sender { get; set; }

        public Guid? ReceiverId { get; set; }

        [Ignore]
        public Character Receiver { get; set; }

        public string Subject { get; set; }

        [Ignore]
        public virtual ICollection<Message> Messages { get; set; }

        public DateTime? LastMessageTimestamp { get; set; }

        [Ignore]
        public string TableName { get; } = TableNames.ChatTable;
    }
}