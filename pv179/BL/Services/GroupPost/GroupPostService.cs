using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Filters;
using BL.Services.Common;

namespace BL.Services.GroupPost
{
    public class GroupPostService : CrudQueryServiceBase<Game.DAL.Entity.Entities.GroupPost, GroupPostDto, GroupFilterDto>
    {
    }
}
