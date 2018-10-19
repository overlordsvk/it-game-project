using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.PetaPoco
{
    [TableName(TableNames.MessageTable)]
    public class Message : IEntity
    {
        public int Id { get; set; }

        public int? SenderId { get; set; }
        [Ignore]
        public Character Sender { get; set; }

        public int? ReceiverId { get; set; }
        [Ignore]
        public Character Receiver { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }


    }
}
