using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class GroupDto : DtoBase
    {
        public string Name { get; set; }

        public ICollection<CharacterDto> Members { get; set; }

        public ICollection<GroupPostDto> Wall { get; set; }

        public string Description { get; set; }
    }
}
