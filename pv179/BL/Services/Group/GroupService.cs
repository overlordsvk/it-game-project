using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.QueryObject;
using BL.Services.Common;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using Game.Infrastructure.Query;

namespace BL.Services.Group
{
    public class GroupService : CrudQueryServiceBase<Group, GroupDto, GroupFilterDto>, IGroupService
    {
        protected override async Task<Group> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Group.Members), nameof(Group.Wall));
        }

        public GroupService(IMapper mapper, IRepository<Group> repository, QueryObjectBase<Group, GroupDto, GroupFilterDto, IQuery<Group>> query) : base(mapper, repository, query)
        {
        }

        public async Task<QueryResultDto<GroupDto, GroupFilterDto>> ListFightsAsync(GroupFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
