using BL.DTO.Common;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class GroupPostDto : DtoBase
    {
        public Guid? CharacterId { get; set; }

        [JsonIgnore]
        public CharacterDto Author { get; set; }

        [MaxLength(4096)]
        public string Text { get; set; }

        public Guid GroupId { get; set; }

        [JsonIgnore]
        public GroupDto Group { get; set; }

        public DateTime Timestamp { get; set; }
    }
}