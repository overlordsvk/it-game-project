using BL.DTO.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class ChatDto : DtoBase
    {
        [Required]
        [MaxLength(256, ErrorMessage = "Predmet nesmie mať viac ako 256 znakov")]
        [MinLength(4, ErrorMessage = "Predmet nesmie mať menej ako 4 znaky")]
        [DisplayName("Predmet")]
        public string Subject { get; set; }

        [JsonIgnore]
        public ICollection<MessageDto> Messages { get; set; }

        [DisplayName("Čas poslednej správy")]
        public DateTime? LastMessageTimestamp { get; set; }

        [DisplayName("Odosielateľ")]
        public Guid? SenderId { get; set; }

        public CharacterDto Sender { get; set; }

        [DisplayName("Prijímateľ")]
        public Guid? ReceiverId { get; set; }

        public CharacterDto Receiver { get; set; }
    }
}