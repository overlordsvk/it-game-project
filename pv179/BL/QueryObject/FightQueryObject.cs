using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using Game.DAL.Entity.Entities;
using Game.Infrastructure.Query;
using Game.Infrastructure.Query.Predicates;
using Game.Infrastructure.Query.Predicates.Operators;
using System.Collections.Generic;
using System.Linq;

namespace BL.QueryObject
{
    public class FightQueryObject : QueryObjectBase<FightDto, Fight, FightFilterDto, IQuery<Fight>>
    {
        public FightQueryObject(IMapper mapper, IQuery<Fight> query) : base(mapper, query)
        {
        }

        protected override IQuery<Fight> ApplyWhereClause(IQuery<Fight> query, FightFilterDto filter)
        {
            var predicates = new List<IPredicate>();
            AddIfDefined(FilterAttackerId(filter), predicates);
            AddIfDefined(FilterDefenderId(filter), predicates);
            AddIfDefined(FilterSuccess(filter), predicates);
            AddIfDefined(FilterFighterId(filter), predicates);

            if (predicates.Count == 0)
            {
                return query;
            }
            if (predicates.Count == 1)
            {
                return query.Where(predicates.First());
            }
            var wherePredicate = new CompositePredicate(predicates);
            return query.Where(wherePredicate);
        }

        private IPredicate FilterSuccess(FightFilterDto filter)
        {
            return !filter.AttackSuccess.HasValue
                ? null
                : new SimplePredicate(nameof(Fight.AttackSuccess),
                    ValueComparingOperator.Equal,
                    filter.AttackSuccess.Value);
        }

        private IPredicate FilterDefenderId(FightFilterDto filter)
        {
            return !filter.DefenderId.HasValue
                ? null
                : new SimplePredicate(nameof(Fight.DefenderId),
                    ValueComparingOperator.Equal,
                    filter.DefenderId.Value);
        }

        private IPredicate FilterFighterId(FightFilterDto filter)
        {
            if (!filter.FighterId.HasValue)
            {
                return null;
            }
            else
            {
                var fights = new List<IPredicate>();
                fights.Add(new SimplePredicate(nameof(Fight.DefenderId),
                    ValueComparingOperator.Equal,
                    filter.FighterId.Value));
                fights.Add(new SimplePredicate(nameof(Fight.AttackerId),
                    ValueComparingOperator.Equal,
                    filter.FighterId.Value));
                return new CompositePredicate(fights, LogicalOperator.OR);
            }
        }

        private IPredicate FilterAttackerId(FightFilterDto filter)
        {
            return !filter.AttackerId.HasValue
                ? null
                : new SimplePredicate(nameof(Fight.AttackerId),
                    ValueComparingOperator.Equal,
                    filter.AttackerId.Value);
        }

        private static void AddIfDefined(IPredicate categoryPredicate, ICollection<IPredicate> definedPredicates)
        {
            if (categoryPredicate != null)
            {
                definedPredicates.Add(categoryPredicate);
            }
        }
    }
}