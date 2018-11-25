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
        private readonly Random _random = new Random();

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
            var r = res.Items.SingleOrDefault();
            Console.WriteLine(r?.Name);
            return r;
        }

        public async Task<ItemDto> GetEquippedArmor(Guid id)
        {
            var res = await ListItemsAsync(new ItemFilterDto(){OwnerId = id, IsEquipped = true, ItemType = ItemType.Armor});
            return res.Items.SingleOrDefault();
        }

        public ItemDto GetNewItem()
        {
            var attack = _random.Next(1,500);
            var defense = _random.Next(1,500);
            var type = _random.Next(2);
            var itemType = ItemType.Armor;
            if (type > 0)
                itemType = ItemType.Weapon;
            var price = (attack + defense + 31) * _random.Next(1,5);
            var name = GenerateName(_random.Next(3,10)) + " " + GenerateName(_random.Next(6,16));

            var newItem = new ItemDto()
            {
                Attack = attack,
                Defense = defense,
                Equipped = false,
                ItemType = itemType,
                Name = name,
                Weight = _random.Next(1,11),
                Price = price
            };

            if (newItem.ItemType == ItemType.Weapon && newItem.Attack < newItem.Defense)
            { 
                newItem.Attack = defense;
                newItem.Defense = attack;
            }
            if (newItem.ItemType == ItemType.Armor && newItem.Attack > newItem.Defense)
            { 
                newItem.Attack = defense;
                newItem.Defense = attack;
            }
            return newItem;
        }

        public async Task<bool> EquipItem(Guid characterId, Guid itemId)
        {
            var item = await Repository.GetAsync(itemId);
            if (item == null || !characterId.Equals(item.OwnerId))
                return false;

            ItemDto equppedItem;
            if(item.ItemType == ItemType.Armor)
                equppedItem = await GetEquippedArmor(characterId);
            else
                equppedItem = await GetEquippedWeapon(characterId);

            if (equppedItem != null)
            {
                equppedItem.Equipped = false;
                await Update(equppedItem);
            }

            item.Equipped = true;
            Repository.Update(item);
            return true;
        }

        // copy-paste from https://stackoverflow.com/questions/14687658/random-name-generator-in-c-sharp
        private string GenerateName(int len)
        { 
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[_random.Next(consonants.Length)].ToUpper();
            Name += vowels[_random.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[_random.Next(consonants.Length)];
                b++;
                Name += vowels[_random.Next(vowels.Length)];
                b++;
            }
            return Name;
        }


    }
}
