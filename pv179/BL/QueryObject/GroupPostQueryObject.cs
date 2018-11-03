using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using Game.DAL.Entity.Entities;
using Game.Infrastructure.Query;
using Game.Infrastructure.Query.Predicates;
using Game.Infrastructure.Query.Predicates.Operators;

namespace BL.QueryObject
{
    public class GroupPostQueryObject : QueryObjectBase<GroupPostDto, GroupPost, GroupPostFilterDto, IQuery<GroupPost>>
    {
        public GroupPostQueryObject(IMapper mapper, IQuery<GroupPost> query) : base(mapper, query)
        {
        }

        protected override IQuery<GroupPost> ApplyWhereClause(IQuery<GroupPost> query, GroupPostFilterDto filter)
        {
            return !filter.GroupId.HasValue
                ? query
                : query.Where(new SimplePredicate(nameof(GroupPost.GroupId),
                    ValueComparingOperator.Equal,
                    filter.GroupId.Value));
        }
    }
}
