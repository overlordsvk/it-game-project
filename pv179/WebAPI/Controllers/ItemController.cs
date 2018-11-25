using BL.DTO;
using BL.DTO.Filters;
using BL.Facades;
using Game.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ItemController : ApiController
    {
        public CharacterFacade CharacterFacade { get; set; }

        /// <summary>
        /// Example URL call: http://localhost:62677/api/Item/?characterId=3454b2db-4bb1-4ffa-a3e9-be8c9617252d
        /// Returns Luk (according to initial DB)
        /// <summary>
        /// <param name="sort">Name of the attribute (e.g. "characterId", "isEquipped", ...) to sort according to</param>
        /// <param name="asc">Sort collection in ascending manner</param>
        /// <param name="characterId">Guid of given character</param>
        /// <param name="isEquipped">Is this item equipped</param>
        /// <param name="pageSize">How many results per req.</param>
        /// <param name="pageNumber">Page number</param>
        /// <returns>Collection of Items, satisfying given query parameters.</returns>
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> Get(string sort = null, 
                                                        bool asc = true, 
                                                        string characterId = null, 
                                                        bool? isEquipped = null, 
                                                        int pageSize = 50, 
                                                        int pageNumber = 1)
        {
            var filter = new ItemFilterDto
            {
                SortCriteria = sort,
                SortAscending = asc,
                PageSize = pageSize,
                RequestedPageNumber = pageNumber
            };
            if (isEquipped.HasValue)
                filter.IsEquipped = isEquipped.Value;

            if (characterId != null)
                filter.OwnerId = Guid.Parse(characterId);

            var characters = (await CharacterFacade.GetItemsByFilterAsync(filter)).Items;
            return characters;
        }


        /// <summary>
        /// Example URL call: http://localhost:62677/api/Item/?characterId=b49c2b2f-99b0-4f21-8655-70b0d2c7c5d2&attack=400&defense=121&price=6060&type=0&weight=100&randomItem=false&name=Special Rare Weapon
        /// Returns item created
        /// <summary>
        /// <param name="characterId">Guid of character to give item</param>
        /// <params name="itemDto">Params of item dto given or default</params>
        /// <returns>ItemDto</returns>
        [HttpPut]
        public async Task<ItemDto> Put(string characterId = null, bool randomItem = false , int attack = 10, int defense = 10, 
                                        int type = 0, string name = "New Item", int price = 20, int weight = 1)
        {
            if (characterId == null)
                return new ItemDto();
            var characterGuid = Guid.Parse(characterId);
            if (await CharacterFacade.GetCharacterById(characterGuid) == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            ItemDto item = null;
            if (!randomItem)
            {
                type = Math.Abs(type)  % 2;
                var itemType = ItemType.Weapon;
                if (type > 0)
                itemType = ItemType.Armor;
                item = new ItemDto{ 
                    Attack = attack,
                    Defense = defense,
                    ItemType = itemType,
                    Name = name,
                    Price = price,
                    Weight = weight
                };
            }

            var itemId = (await CharacterFacade.GiveItemAsync(characterGuid, item));
            return await CharacterFacade.GetItem(itemId);
        }

        /// <summary>
        /// Example URL call: http://localhost:62677/api/Item/?id=629fd444-0146-4b94-a928-e62a4ab51f42
        /// Sells item
        /// <summary>
        /// <param name="id">Guid of item to sell</param>
        /// <returns>Sold item with id {id}</returns>
        [HttpDelete]
        public async Task<string> Delete(Guid id)
        {
            var res = await CharacterFacade.SellItem(id);
            if (!res)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return $"Sold item with id: {id}";
        }
    }
}
