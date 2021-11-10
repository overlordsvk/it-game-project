using AutoMapper;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.QueryObject;
using BL.Services.Common;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using Game.Infrastructure.Query;
using System;
using System.Threading.Tasks;

namespace BL.Services.Groups
{
    public class GroupService : CrudQueryServiceBase<Group, GroupDto, GroupFilterDto>, IGroupService
    {
        protected override async Task<Group> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Group.Members), nameof(Group.Wall));
        }

        public async Task<QueryResultDto<GroupDto, GroupFilterDto>> ListGroupsAsync(GroupFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        public GroupService(IMapper mapper, IRepository<Group> repository, QueryObjectBase<GroupDto, Group, GroupFilterDto, IQuery<Group>> query) : base(mapper, repository, query)
        {
        }
    }
}