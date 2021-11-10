using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using System;
using System.Threading.Tasks;

namespace BL.Services.Fights
{
    public interface IFightService
    {
        /// <summary>
        /// Gets fights according to given filter
        /// </summary>
        /// <param name="filter">The fights filter</param>
        /// <returns>Filtered results</returns>
        Task<QueryResultDto<FightDto, FightFilterDto>> ListFightsAsync(FightFilterDto filter);

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<FightDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(FightDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(FightDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<FightDto, FightFilterDto>> ListAllAsync();
    }
}