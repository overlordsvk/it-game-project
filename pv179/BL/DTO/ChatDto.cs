using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Common;
using Game.DAL.Entities;

namespace BL.DTO
{
    public class ChatDto : DtoBase
    {
        [MaxLength(256)]
        public string Subject { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public DateTime? LastMessageTimestamp { get; set; }
    }
}
