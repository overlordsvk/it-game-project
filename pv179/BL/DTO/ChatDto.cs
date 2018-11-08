using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BL.DTO.Common;

namespace BL.DTO
{
    public class ChatDto : DtoBase
    {
        [MaxLength(256)]
        public string Subject { get; set; }

        public virtual ICollection<MessageDto> Messages { get; set; }

        public DateTime? LastMessageTimestamp { get; set; }

        public Guid? SenderId { get; set; }

        public Guid? ReceiverId { get; set; }
    }
}
