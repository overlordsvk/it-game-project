﻿using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using System;
using System.Threading.Tasks;

namespace BL.Services.Messages
{
    public interface IMessageService
    {
        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<MessageDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(MessageDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(MessageDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets messages according to given filter
        /// </summary>
        /// <param name="filter">The message filter</param>
        /// <returns>Filtered results</returns>
        Task<QueryResultDto<MessageDto, MessageFilterDto>> ListMessagesAsync(MessageFilterDto filter);
    }
}