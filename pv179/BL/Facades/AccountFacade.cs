using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.Facades.Common;
using BL.Services.Accounts;
using BL.Services.Characters;
using BL.Services.Chats;
using BL.Services.Fights;
using Game.Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class AccountFacade : FacadeBase
    {
        private readonly IAccountService _accountService;
        private readonly IChatService _chatService;
        private readonly ICharacterService _characterService;

        public AccountFacade(IUnitOfWorkProvider unitOfWorkProvider, IAccountService accountService, IChatService chatService, ICharacterService characterService) : base(unitOfWorkProvider)
        {
            this._accountService = accountService;
            this._chatService = chatService;
            this._characterService = characterService;
        }

        /// <summary>
        /// Gets account according to email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Account with specified email</returns>
        public async Task<AccountDto> GetAccountAccordingToEmailAsync(string email)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _accountService.GetAccountAccordingToEmailAsync(email);
            }          
        }

        /// <summary>
        /// Gets account according to username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Account with specified username</returns>
        public async Task<AccountDto> GetAccountAccordingToUsernameAsync(string username)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _accountService.GetAccountAccordingToUsernameAsync(username);
            }          
        }

        /// <summary>
        /// Gets all accounts according to page
        /// </summary>
        /// <returns>all customers</returns>
        public async Task<QueryResultDto<AccountDto, AccountFilterDto>> GetAllAccountsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _accountService.ListAllAsync();
            }
        }

        /// <summary>
        /// Remove account according to accountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>true if account was removed</returns>
        public async Task<bool> RemoveAccountAsync(Guid accountId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (_accountService.GetAsync(accountId).Result == null)
                {
                    return false;
                }
                _accountService.Delete(accountId);
                await uow.Commit();
                if (_accountService.GetAsync(accountId).Result != null)
                {
                    return false;
                }
                return true;
            }
        }


        /// <summary>
        /// Performs account registration
        /// </summary>
        /// <param name="registrationDto">Account registration details</param>
        /// <returns>Registered account ID</returns>
        public async Task<Guid> RegisterAccount(AccountCreateDto registrationDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    var id = await _accountService.RegisterAccountAsync(registrationDto);
                    await uow.Commit();
                    return id;
                } catch (ArgumentException)
                {
                    throw;
                }
            }

             
        }

        public async Task<(bool success, Guid id, string roles)> Login(string usernameOrEmail, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _accountService.AuthorizeAccountAsync(usernameOrEmail, password);
            }
        }


    }
}
