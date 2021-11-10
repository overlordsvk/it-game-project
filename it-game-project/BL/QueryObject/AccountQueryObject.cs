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
    public class AccountQueryObject : QueryObjectBase<AccountDto, Account, AccountFilterDto, IQuery<Account>>
    {
        public AccountQueryObject(IMapper mapper, IQuery<Account> query) : base(mapper, query)
        {
        }

        protected override IQuery<Account> ApplyWhereClause(IQuery<Account> query, AccountFilterDto filter)
        {
            var predicates = new List<IPredicate>();
            AddIfDefined(FilterUsername(filter), predicates);
            AddIfDefined(FilterEmail(filter), predicates);

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

        private IPredicate FilterEmail(AccountFilterDto filter)
        {
            return string.IsNullOrWhiteSpace(filter.Email)
                ? null
                : new SimplePredicate(nameof(Account.Email),
                    ValueComparingOperator.Equal,
                    filter.Email);
        }

        private IPredicate FilterUsername(AccountFilterDto filter)
        {
            return string.IsNullOrWhiteSpace(filter.Username)
                ? null
                : new SimplePredicate(nameof(Account.Username),
                    ValueComparingOperator.Equal,
                    filter.Username);
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