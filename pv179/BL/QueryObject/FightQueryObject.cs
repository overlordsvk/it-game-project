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
    public class FightQueryObject : QueryObjectBase<FightDto, Fight, FightFilterDto, IQuery<Fight>>
    {
        public FightQueryObject(IMapper mapper, IQuery<Fight> query) : base(mapper, query) { }

        protected override IQuery<Fight> ApplyWhereClause(IQuery<Fight> query, FightFilterDto filter)
        {
            var attackerPredicate = new SimplePredicate(nameof(Fight.AttackerId),
                ValueComparingOperator.Equal,
                filter.AttackerId);
            if (!filter.AttackSuccess.HasValue)
            {
                return query.Where(attackerPredicate);
            }
            var predicate = new CompositePredicate(new List<IPredicate>(){attackerPredicate, new SimplePredicate(nameof(Fight.AttackSuccess),
                                                                                            ValueComparingOperator.Equal, 
                                                                                            filter.AttackSuccess.Value)});
            return query.Where(predicate);
        }
    }
}
