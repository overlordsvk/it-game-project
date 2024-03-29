﻿using BL.DTO.Common;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class MessageDto : DtoBase
    {
        public Guid ChatId { get; set; }
        public ChatDto Chat { get; set; }

        [MaxLength(4096, ErrorMessage = "Správa nesmie mať viac ako 256 znakov")]
        public string Text { get; set; }

        public Guid? AuthorId { get; set; }

        [JsonIgnore]
        [DisplayName("Autor")]
        public CharacterDto Author { get; set; }

        [DisplayName("Čas odoslania")]
        public DateTime Timestamp { get; set; }
    }
}