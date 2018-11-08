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

        public ICollection<MessageDto> Messages { get; set; }

        public DateTime? LastMessageTimestamp { get; set; }

        public int? SenderId { get; set; }

        public int? ReceiverId { get; set; }
    }
}
