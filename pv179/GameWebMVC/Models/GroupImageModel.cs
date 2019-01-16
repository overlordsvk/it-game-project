using BL.DTO;
using System.Web;

namespace GameWebMVC.Models
{
    public class GroupImageModel
    {
        public GroupDto Group { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}