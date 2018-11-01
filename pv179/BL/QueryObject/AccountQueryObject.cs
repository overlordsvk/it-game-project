using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.QueryObject;
using Game.DAL.Entity.Entities;
using Game.Infrastructure.Query;
using Game.Infrastructure.Query.Predicates;
using Game.Infrastructure.Query.Predicates.Operators;

namespace BL.QueryObject
{
    public class AccountQueryObject : QueryObjectBase<AccountDto, Account, AccountFilterDto, IQuery<Account>>
    {
        public AccountQueryObject(IMapper mapper, IQuery<Account> query) : base(mapper, query) { }

        protected override IQuery<Account> ApplyWhereClause(IQuery<Account> query, AccountFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Email))
            {
                return query;
            }
            var emailPredicate = new SimplePredicate(nameof(Account.Email),
                                                    ValueComparingOperator.Equal,
                                                    filter.Email);
            return query.Where(emailPredicate);
        }
    }
}
