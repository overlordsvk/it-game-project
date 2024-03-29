﻿using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.Facades.Common;
using BL.Services.Accounts;
using BL.Services.Characters;
using BL.Services.Chats;
using Game.Infrastructure.UnitOfWork;
using System;
using System.Threading.Tasks;

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

        public async Task<QueryResultDto<AccountDto, AccountFilterDto>> ListAccountsAsync(AccountFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _accountService.ListAccountsAsync(filter);
            }
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
        /// Gets account according to id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Account with specified id</returns>
        public async Task<AccountDto> GetAccountAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _accountService.GetAsync(id);
            }
        }

        /// <summary>
        /// Change username of given acc id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        public async Task EditAccountAsync(Guid id, AccountCreateDto updatedAccount)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    var acc = await _accountService.GetAsync(id);
                    acc.Username = updatedAccount.Username;
                    acc.Email = updatedAccount.Email;
                    acc.Roles = updatedAccount.Roles;
                    await _accountService.UpdateAccount(acc, updatedAccount.Password);
                    await uow.Commit();
                }
                catch (NullReferenceException)
                {
                    throw;
                }
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
                }
                catch (ArgumentException)
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