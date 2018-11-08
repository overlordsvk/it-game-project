using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class GroupPostDto : DtoBase
    {
        public Guid? CharacterId { get; set; }
        public CharacterDto Author { get; set; }

        [MaxLength(1024)]
        public string Text { get; set; }

        public Guid GroupId { get; set; }
        public GroupDto Group { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
