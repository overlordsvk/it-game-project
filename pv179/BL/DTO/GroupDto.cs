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
    public class GroupDto : DtoBase
    {
        [Required, StringLength(64, MinimumLength = 4, ErrorMessage = "Názov skupiny musí mať 4 až 64 znakov")]
        public string Name { get; set; }

        [StringLength(512, ErrorMessage = "Skúste iný obrázok")]
        public string Picture { get; set; }

        public ICollection<CharacterDto> Members { get; set; }
        [JsonIgnore]
        public ICollection<GroupPostDto> Wall { get; set; }

        [StringLength(2048, ErrorMessage = "Popis skupiny môže mať max. 2048 znakov")]
        public string Description { get; set; }
    }
}
