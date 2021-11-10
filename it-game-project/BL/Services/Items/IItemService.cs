using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using System;
using System.Threading.Tasks;

namespace BL.Services.Items
{
    public interface IItemService
    {
        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<ItemDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(ItemDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(ItemDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets items according to given filter
        /// </summary>
        /// <param name="filter">The item filter</param>
        /// <returns>Filtered results</returns>
        Task<QueryResultDto<ItemDto, ItemFilterDto>> ListItemsAsync(ItemFilterDto filter);

        /// <summary>
        /// Gets equipped weapon
        /// </summary>
        /// <param name="id">Character id</param>
        /// <returns>Equipped weapon</returns>
        Task<ItemDto> GetEquippedWeapon(Guid id);

        /// <summary>
        /// Gets equipped armor
        /// </summary>
        /// <param name="id">Character id</param>
        /// <returns>Equipped armor</returns>
        Task<ItemDto> GetEquippedArmor(Guid id);

        /// <summary>
        /// Create new item
        /// </summary>
        /// <returns>New item</returns>
        ItemDto GetNewItem();

        /// <summary>
        /// Equip Item
        /// </summary>
        /// <param name="characterId">Character id</param>
        /// <param name="itemId">Item id</param>
        /// <returns>True if item was equipped</returns>
        Task<bool> EquipItem(Guid characterId, Guid itemId);
    }
}