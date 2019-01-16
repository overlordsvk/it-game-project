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

namespace BL.Services.GroupPosts
{
    public class GroupPostService : CrudQueryServiceBase<GroupPost, GroupPostDto, GroupPostFilterDto>, IGroupPostService
    {
        public GroupPostService(IMapper mapper, IRepository<GroupPost> repository, QueryObjectBase<GroupPostDto, GroupPost, GroupPostFilterDto, IQuery<GroupPost>> query) : base(mapper, repository, query)
        {
        }

        protected override async Task<GroupPost> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(GroupPost.Author), nameof(GroupPost.Group));
        }

        async Task<QueryResultDto<GroupPostDto, GroupPostFilterDto>> IGroupPostService.ListGroupPostsAsync(GroupPostFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}