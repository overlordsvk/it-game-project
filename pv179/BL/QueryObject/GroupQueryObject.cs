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
    public class GroupQueryObject : QueryObjectBase<GroupDto, Group, GroupFilterDto, IQuery<Group>>
    {
        public GroupQueryObject(IMapper mapper, IQuery<Group> query) : base(mapper, query)
        {
        }

        protected override IQuery<Group> ApplyWhereClause(IQuery<Group> query, GroupFilterDto filter)
        {

            return string.IsNullOrWhiteSpace(filter.Name) 
                ? query 
                : query.Where(new SimplePredicate(nameof(Group.Name),
                                                ValueComparingOperator.StringContains,
                                                filter.Name));
        }
    }
}
