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
        [Required, MinLength(4, ErrorMessage = "Názov skupiny musí mať aspoň 4 znaky"), MaxLength(64, ErrorMessage = "Názov skupiny nesmie mať viac ako 64 znakov")]
        public string Name { get; set; }

        [StringLength(512, ErrorMessage = "Skúste iný obrázok")]
        public string Picture { get; set; }

        public ICollection<CharacterDto> Members { get; set; }
        [JsonIgnore]
        public ICollection<GroupPostDto> Wall { get; set; }

        [StringLength(4096, ErrorMessage = "Popis skupiny môže mať max. 4096 znakov")]
        public string Description { get; set; }
    }
}
