using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;

namespace BL.Services.Characters
{
    public interface ICharacterService
    {
        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<CharacterDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(CharacterDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(CharacterDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<CharacterDto, CharacterFilterDto>> ListAllAsync();

        /// <summary>
        /// Gets characters according to given filter
        /// </summary>
        /// <param name="filter">The character filter</param>
        /// <returns>Filtered results</returns>
        Task<QueryResultDto<CharacterDto, CharacterFilterDto>> ListCharactersAsync(CharacterFilterDto filter);
    }
}
