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
        /// <returns>Account with given email address</returns>
        Task<AccountDto> GetAccountAccordingToEmailAsync(string email);

        /// <summary>
        /// Gets account with given username address
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>Account with given username</returns>
        Task<AccountDto> GetAccountAccordingToUsernameAsync(string username);

        /// <summary>
        /// Registers account
        /// </summary>
        /// <param name="accountCreate">accountCreate</param>
        /// <returns>Registers account</returns>
        int Register(AccountCreateDto accountCreate);
        
        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<AccountDto> GetAsync(int entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        int Create(AccountDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(AccountDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(int entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<AccountDto, AccountFilterDto>> ListAllAsync();
    }
}
