using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;

namespace BL.Services.Accounts
{
    public interface IAccountService
    {
        /// <summary>
        /// Gets account with given email address
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>AccountDto with given email address</returns>
        Task<AccountDto> GetAccountAccordingToEmailAsync(string email);

        /// <summary>
        /// Gets account with given username address
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>AccountDto with given username</returns>
        Task<AccountDto> GetAccountAccordingToUsernameAsync(string username);
        
        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The AccountDto representing the entity</returns>
        Task<AccountDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity to create</param>
        /// <returns>Guid of created entity</returns>
        Guid Create(AccountDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(AccountDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<AccountDto, AccountFilterDto>> ListAllAsync();

        /// <summary>
        /// Register new account
        /// </summary>
        /// <param name="account">account to create</param>
        /// <returns>Guid of registered account</returns>
        Task<Guid> RegisterAccountAsync(AccountCreateDto account);

        /// <summary>
        /// Authorize account
        /// </summary>
        /// <param name="usernameOrEmail">Username or email of account</param>
        /// <param name="password">Password</param>
        /// <returns>Autorization success, guid of authorized account and account roles </returns>
        Task<(bool success, Guid id, string roles)> AuthorizeAccountAsync(string usernameOrEmail, string password);



    }
}
