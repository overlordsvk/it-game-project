﻿using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using System;
using System.Threading.Tasks;

namespace BL.Services.GroupPosts
{
    public interface IGroupPostService
    {
        /// <summary>
        /// Gets group posts according to given filter
        /// </summary>
        /// <param name="filter">The fights filter</param>
        /// <returns>Filtered results</returns>
        Task<QueryResultDto<GroupPostDto, GroupPostFilterDto>> ListGroupPostsAsync(GroupPostFilterDto filter);

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<GroupPostDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(GroupPostDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(GroupPostDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);
    }
}