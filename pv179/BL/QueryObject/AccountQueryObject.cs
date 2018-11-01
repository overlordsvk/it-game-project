using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Filters;
using BL.QueryObject;
using Game.DAL.Entity.Entities;
using Infrastructure.Query;

namespace BL.QueryObject
{
    class AccountQueryObject : QueryObjectBase<AccountDto, Account, AccountFilterDto IQuery<Account>>
    {
        protected override IQuery<Account> ApplyWhereClause(IQuery<Account> query, AccountFilterDto filter)
        {
            throw new NotImplementedException();
        }
    }
}
