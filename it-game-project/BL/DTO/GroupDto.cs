﻿using BL.DTO.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class GroupDto : DtoBase
    {
        [Required(ErrorMessage = "Meno skupiny nesmie byť prázdne")]
        [MinLength(4, ErrorMessage = "Názov skupiny musí mať aspoň 4 znaky")]
        [MaxLength(64, ErrorMessage = "Názov skupiny nesmie mať viac ako 64 znakov")]
        [DisplayName("Meno skupiny")]
        public string Name { get; set; }

        [StringLength(512, ErrorMessage = "Skúste iný obrázok")]
        [DisplayName("Obrázok")]
        public string Picture { get; set; }

        public ICollection<CharacterDto> Members { get; set; }

        [JsonIgnore]
        public ICollection<GroupPostDto> Wall { get; set; }

        [StringLength(4096, ErrorMessage = "Popis skupiny môže mať max. 4096 znakov")]
        [DisplayName("Popis")]
        public string Description { get; set; }
    }
}