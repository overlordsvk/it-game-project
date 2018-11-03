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
    public class CharacterQueryObject : QueryObjectBase<CharacterDto, Character, CharacterFilterDto, IQuery<Character>>
    {
        public CharacterQueryObject(IMapper mapper, IQuery<Character> query) : base(mapper, query)
        {
        }

        protected override IQuery<Character> ApplyWhereClause(IQuery<Character> query, CharacterFilterDto filter)
        {
            var predicates = new List<IPredicate>();
            AddIfDefined(FilterCharacterName(filter), predicates);
            AddIfDefined(FilterGroup(filter), predicates);
            AddIfDefined(FilterScore(filter), predicates);

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

        private IPredicate FilterScore(CharacterFilterDto filter)
        {
            return new CompositePredicate(new List<IPredicate>
            {
                new SimplePredicate(nameof(Character.Score), ValueComparingOperator.GreaterThanOrEqual,
                    filter.ScoreMin),
                new SimplePredicate(nameof(Character.Score), ValueComparingOperator.LessThanOrEqual, filter.ScoreMax)
            });
        }

        private IPredicate FilterGroup(CharacterFilterDto filter)
        {
            return !filter.GroupId.HasValue
                ? null
                : new SimplePredicate(nameof(Character.GroupId),
                    ValueComparingOperator.Equal,
                    filter.GroupId.Value);
        }

        private IPredicate FilterCharacterName(CharacterFilterDto filter)
        {
            return string.IsNullOrWhiteSpace(filter.Name)
                ? null
                : new SimplePredicate(nameof(Character.Name),
                    ValueComparingOperator.StringContains,
                    filter.Name);
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