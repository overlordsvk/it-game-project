using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.QueryObject;
using BL.Services.Common;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using Game.Infrastructure.Query;

namespace BL.Services.Accounts
{
    public class AccountService : CrudQueryServiceBase<Account, AccountDto, AccountFilterDto>, IAccountService
    {
        public AccountService(IMapper mapper, IRepository<Account> repository, QueryObjectBase<AccountDto, Account, AccountFilterDto, IQuery<Account>> query) : base(mapper, repository, query)
        {
        }

        protected override async Task<Account> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Character));
        }

        public async Task<AccountDto> GetAccountAccordingToEmailAsync(string email)
        {
            var queryResult = await Query.ExecuteQuery(new AccountFilterDto{Email = email});
            return queryResult.Items.SingleOrDefault();
        }
        

        public async Task<AccountDto> GetAccountAccordingToUsernameAsync(string username)
        {
            var queryResult = await Query.ExecuteQuery(new AccountFilterDto{Username = username});
            return queryResult.Items.SingleOrDefault();
        }
    }
}
