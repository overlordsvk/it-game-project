using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.QueryObject;
using BL.Services.Common;
using Game.DAL.Entity.Entities;
using Game.DAL.Enums;
using Game.Infrastructure;
using Game.Infrastructure.Query;

namespace BL.Services.Items
{
    public class ItemService : CrudQueryServiceBase<Item, ItemDto, ItemFilterDto>, IItemService
    {
        public ItemService(IMapper mapper, IRepository<Item> repository, QueryObjectBase<ItemDto, Item, ItemFilterDto, IQuery<Item>> query) : base(mapper, repository, query)
        {
        }

        protected override async Task<Item> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Item.Owner));
        }

        public async Task<QueryResultDto<ItemDto, ItemFilterDto>> ListItemsAsync(ItemFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        public async Task<ItemDto> GetEquippedWeapon(Guid id)
        {
            var res = await ListItemsAsync(new ItemFilterDto(){OwnerId = id, IsEquipped = true, ItemType = ItemType.Weapon});
            return res.Items.SingleOrDefault();
        }

        public async Task<ItemDto> GetEquippedArmor(Guid id)
        {
            var res = await ListItemsAsync(new ItemFilterDto(){OwnerId = id, IsEquipped = true, ItemType = ItemType.Armor});
            return res.Items.SingleOrDefault();
        }

        public ItemDto GetNewItem()
        {
            var newItem = new ItemDto()
            {
                Attack = 5,
                Defense = 4,
                Equipped = false,
                ItemType = ItemType.Weapon,
                Name = "New Item Name",
                Weight = 3,
                Price = 310
            };
            return newItem;
        }
    }
}
