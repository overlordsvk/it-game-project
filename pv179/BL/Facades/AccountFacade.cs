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
        private readonly IFightService _fightService;

        public AccountFacade(IUnitOfWorkProvider unitOfWorkProvider, IAccountService accountService, IChatService chatService, ICharacterService characterService, IFightService fightService) : base(unitOfWorkProvider)
        {
            this._accountService = accountService;
            this._chatService = chatService;
            this._characterService = characterService;
            _fightService = fightService;
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
        /// <returns>0 if account was removed</returns>
        public async Task<int> RemoveAccountAsync(int accountId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (_accountService.GetAsync(accountId).Result == null)
                {
                    return -1;
                }
                //var character = _characterService.GetAsync(accountId).Result;
                //checkNULL character
                //character.ReceiverChats = null;
                //character.SenderChats = null;
                //**_fightService.RemoveFightCharacterConnections(accountId);
                //_characterService.Delete(accountId);
                //await uow.Commit();
                //_chatService.RemoveReferencesToCharacterAsync(accountId);
                //_accountService
                _accountService.Delete(accountId);
                await uow.Commit();
                if (_accountService.GetAsync(accountId).Result == null)
                {
                    return -2;
                }
                return 0;
            }
        }


        /// <summary>
        /// Performs account registration
        /// </summary>
        /// <param name="registrationDto">Account registration details</param>
        /// <returns>Registered account ID</returns>
        public async Task<int> RegisterAccount(AccountCreateDto registrationDto)
        {
            int accountId;
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (GetAccountAccordingToEmailAsync(registrationDto.Email).Result != null)
                {
                    return -1;
                }

                if (GetAccountAccordingToUsernameAsync(registrationDto.Username).Result != null)
                {
                    return -2;
                }
                var newAccount = new AccountDto()
                {
                    Username = registrationDto.Username,
                    Email = registrationDto.Email,
                    Password = registrationDto.Password,
                };

                accountId = _accountService.Create(newAccount);
                await uow.Commit();
            }
            return accountId;
             
        }



    }
}
