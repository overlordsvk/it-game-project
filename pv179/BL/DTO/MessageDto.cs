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
        public Guid ChatId { get; set; }
        public ChatDto Chat { get; set; }

        [MaxLength(2048)]
        public string Text { get; set; }

        public Guid? AuthorId { get; set; }
        public CharacterDto Author { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
