using BL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameWebMVC.Models
{
    public class GroupImageModel
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [Required, StringLength(64, MinimumLength = 4, ErrorMessage = "Názov skupiny musí mať 4 až 64 znakov")]
        public string Name { get; set; }

        [StringLength(2048, ErrorMessage = "Popis skupiny môže mať max. 2048 znakov")]
        public string Description { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}