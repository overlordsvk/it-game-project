using BL.Facades.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.Services.Accounts;
using BL.Services.Characters;
using BL.Services.Fights;
using BL.Services.Items;
using Game.DAL.Enums;
using Game.Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class CharacterFacade : FacadeBase
    {
        private readonly IAccountService _accountService;
        private readonly ICharacterService _characterService;
        private readonly IItemService _itemService;
        private readonly IFightService _fightService;

        public CharacterFacade(IUnitOfWorkProvider unitOfWorkProvider, ICharacterService characterService, IAccountService accountService, IItemService itemService, IFightService fightService) : base(unitOfWorkProvider)
        {
            _accountService = accountService;
            _characterService = characterService;
            _itemService = itemService;
            _fightService = fightService;
        }

        /// <summary>
        /// Gets all customers according to page
        /// </summary>
        /// <returns>all customers</returns>
        public async Task<QueryResultDto<CharacterDto, CharacterFilterDto>> GetAllCharactersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _characterService.ListAllAsync();
            }
        }

        /// <summary>
        /// Gets all customers according to page
        /// </summary>
        /// <returns>all customers</returns>
        public async Task<QueryResultDto<CharacterDto, CharacterFilterDto>> GetCharactersByFilterAsync(CharacterFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _characterService.ListCharactersAsync(filter);
            }
        }

        /// <summary>
        /// Gets character according to ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Character by id</returns>
        public async Task<CharacterDto> GetCharacterById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _characterService.GetAsync(id);
            }          
        }

        /// <summary>
        /// Gets inventory of character
        /// </summary>
        /// <returns>all customers</returns>
        public async Task<QueryResultDto<ItemDto, ItemFilterDto>> GetCharacterItems(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.ListItemsAsync(new ItemFilterDto(){OwnerId = id});
            }
        }

        public async Task<ItemDto> GetEquipedWeapon(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.GetEquippedWeapon(id);
            }
        }

        public async Task<ItemDto> GetEquipedArmor(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.GetEquippedArmor(id);
            }
        }

        public async void SellItem(int itemId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var item = _itemService.GetAsync(itemId).Result;
                var ownerId = item.OwnerId;
                if (!ownerId.HasValue)
                    return;
                var owner = _characterService.GetAsync(ownerId.Value).Result;
                owner.Money += item.Price;
                item.OwnerId = null;
                await _characterService.Update(owner);
            }
        }

        public async void EquipItem(int itemId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var item = _itemService.GetAsync(itemId).Result;
                var ownerId = item.OwnerId;
                if (!ownerId.HasValue)
                    return;
                ItemDto equippedItem;
                if (item.ItemType == ItemType.Weapon)
                {
                    equippedItem = await GetEquipedWeapon(ownerId.Value);
                }
                else
                {
                    equippedItem = await GetEquipedArmor(ownerId.Value);
                }

                equippedItem.Equipped = false;
                await _itemService.Update(equippedItem);
                item.Equipped = true;
                await _itemService.Update(item);
            }
        }

        public async Task<bool> BuyItem(int characterId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var character = _characterService.GetAsync(characterId).Result;
                if (character.Money >= 100)
                {

                    return true;
                }

                return false;
                //generate item

                return true;
            }
        }

        public async Task<QueryResultDto<FightDto, FightFilterDto>> GetFightsHistory(FightFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _fightService.ListFightsAsync(filter);
            }
        }
    }
}
