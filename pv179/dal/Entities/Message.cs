using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public Character Sender { get; set; }
        public Character Receiver { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }


    }
}
