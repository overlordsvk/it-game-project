using BL.DTO.Common;
using Newtonsoft.Json;
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
        public Guid ChatId { get; set; }
        public ChatDto Chat { get; set; }

        [MaxLength(4096, ErrorMessage ="Správa nesmie mať viac ako 256 znakov")]
        public string Text { get; set; }

        public Guid? AuthorId { get; set; }
        [JsonIgnore]
        public CharacterDto Author { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
