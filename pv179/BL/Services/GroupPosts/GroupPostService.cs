using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Game.DAL.Entity.Entities;
using BL.DTO;
using BL.DTO.Filters;
using BL.Services.Common;
using BL.DTO.Common;
using BL.QueryObject;
using Game.Infrastructure;
using Game.Infrastructure.Query;

namespace BL.Services.GroupPosts
{
    public class GroupPostService : CrudQueryServiceBase<GroupPost, GroupPostDto, GroupFilterDto>, IGroupPostService
    {
        public async Task<QueryResultDto<GroupPostDto, GroupFilterDto>> ListFightsAsync(GroupFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        protected override async Task<GroupPost> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(GroupPost.Author), nameof(GroupPost.Group));
        }

        public GroupPostService(IMapper mapper, IRepository<GroupPost> repository, QueryObjectBase<GroupPostDto, GroupPost, GroupFilterDto, IQuery<GroupPost>> query) : base(mapper, repository, query)
        {
        }
    }
}
