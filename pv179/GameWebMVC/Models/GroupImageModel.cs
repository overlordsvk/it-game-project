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
        public GroupDto Group { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}