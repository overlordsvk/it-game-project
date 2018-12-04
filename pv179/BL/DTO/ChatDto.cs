using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BL.DTO.Common;
using Newtonsoft.Json;

namespace BL.DTO
{
    public class ChatDto : DtoBase
    {
        [Required, MaxLength(256, ErrorMessage = "Predmet nesmie mať viac ako 256 znakov")]
        public string Subject { get; set; }

        [JsonIgnore]
        public ICollection<MessageDto> Messages { get; set; }

        public DateTime? LastMessageTimestamp { get; set; }

        public Guid? SenderId { get; set; }
        public CharacterDto Sender{ get; set; }

        public Guid? ReceiverId { get; set; }
        public CharacterDto Receiver { get; set; }
    }
}
