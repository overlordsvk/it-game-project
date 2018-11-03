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
    public class ItemQueryObject : QueryObjectBase<ItemDto, Item, ItemFilterDto, IQuery<Item>>
    {
        public ItemQueryObject(IMapper mapper, IQuery<Item> query) : base(mapper, query)
        {
        }

        protected override IQuery<Item> ApplyWhereClause(IQuery<Item> query, ItemFilterDto filter)
        {
            var predicates = new List<IPredicate>();
            AddIfDefined(FilterItemName(filter), predicates);
            AddIfDefined(FilterItemOwner(filter), predicates);
            AddIfDefined(FilterItemType(filter), predicates);
            AddIfDefined(FilterIsEquipped(filter), predicates);

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

        private IPredicate FilterIsEquipped(ItemFilterDto filter)
        {
            return !filter.IsEquipped.HasValue
                ? null
                : new SimplePredicate(nameof(Item.Equipped),
                    ValueComparingOperator.Equal,
                    filter.IsEquipped.Value);
        }

        private IPredicate FilterItemType(ItemFilterDto filter)
        {
            return !filter.ItemType.HasValue
                ? null
                : new SimplePredicate(nameof(Item.ItemType),
                    ValueComparingOperator.Equal,
                    filter.ItemType.Value);
        }

        private IPredicate FilterItemOwner(ItemFilterDto filter)
        {
            return !filter.OwnerId.HasValue
                ? null
                : new SimplePredicate(nameof(Item.OwnerId),
                    ValueComparingOperator.Equal,
                    filter.OwnerId.Value);
        }

        private static SimplePredicate FilterItemName(ItemFilterDto filter)
        {
            return string.IsNullOrWhiteSpace(filter.Name)
                ? null
                : new SimplePredicate(nameof(Item.Name),
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