using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class MessageDto : DtoBase
    {
        public Guid? SenderId { get; set; }
        public CharacterDto Sender { get; set; }

        public Guid? ReceiverId { get; set; }
        public CharacterDto Receiver { get; set; }

        [MaxLength(256)]
        public string Subject { get; set; }

        [MaxLength(2048)]
        public string Text { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
