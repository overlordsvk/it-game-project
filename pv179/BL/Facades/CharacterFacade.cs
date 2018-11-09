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
using BL.Services.CharacterChanges;

namespace BL.Facades
{
    public class CharacterFacade : FacadeBase
    {
        private readonly IAccountService _accountService;
        private readonly ICharacterService _characterService;
        private readonly IItemService _itemService;
        private readonly IFightService _fightService;
        private readonly ICharacterAddMoneyService _characterChangesService;

        public CharacterFacade(IUnitOfWorkProvider unitOfWorkProvider, ICharacterService characterService, IAccountService accountService, IItemService itemService, IFightService fightService, ICharacterAddMoneyService characterChangesService) : base(unitOfWorkProvider)
        {
            _accountService = accountService;
            _characterService = characterService;
            _itemService = itemService;
            _fightService = fightService;
            _characterChangesService = characterChangesService;
        }

        /// <summary>
        /// Gets all characters according to page
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
        public async Task<CharacterDto> GetCharacterById(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _characterService.GetAsync(id);
            }          
        }

        /// <summary>
        /// Gets character according to ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Character by id</returns>
        public Guid CreateCharacter(CharacterDto character)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var characterId = _characterService.Create(character);
                uow.Commit();
                return characterId;
                //TODO
            }
        }

        /// <summary>
        /// Gets inventory of character
        /// </summary>
        /// <returns>all customers</returns>
        public async Task<QueryResultDto<ItemDto, ItemFilterDto>> GetCharacterItems(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.ListItemsAsync(new ItemFilterDto(){OwnerId = id});
            }
        }

        public async Task<ItemDto> GetEquippedWeapon(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.GetEquippedWeapon(id);
            }
        }

        public async Task<ItemDto> GetEquippedArmor(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.GetEquippedArmor(id);
            }
        }

        public async void SellItem(Guid itemId)
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

        public async Task<bool> EquipItem(Guid characterId, Guid itemId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var succ = await _itemService.EquipItem(characterId, itemId);
                await uow.Commit();
                return succ;
                //var item = _itemService.GetAsync(itemId).Result;
                //var ownerId = item.OwnerId;
                //if (!ownerId.HasValue)
                //    return;
                //ItemDto equippedItem;
                //if (item.ItemType == ItemType.Weapon)
                //{
                //    equippedItem = await GetEquipedWeapon(ownerId.Value);
                //}
                //else
                //{
                //    equippedItem = await GetEquipedArmor(ownerId.Value);
                //}

                //equippedItem.Equipped = false;
                //await _itemService.Update(equippedItem);
                //item.Equipped = true;
                //await _itemService.Update(item);
            }
        }

        public async Task<bool> BuyItemAsync(Guid characterId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var character = await _characterService.GetAsync(characterId);
                if (character.Money < 100) return false;

                character.Money -= 100;
                var item =_itemService.GetNewItem();
                item.OwnerId = characterId;
                _itemService.Create(item);
                await _characterService.Update(character);
                await uow.Commit();
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

        public async Task<Guid> Attack(Guid attackerId, Guid defenderId)
        {
            Guid fightId;

            using (var uow = UnitOfWorkProvider.Create())
            {
            var attacker = await _characterService.GetAsync(attackerId);
            var defender = await _characterService.GetAsync(defenderId);

            if (attacker == null)
            {
                return Guid.Empty;
            }
            if (defender == null)
            {
                return Guid.Empty;
            }
            var attackerItem = await GetEquippedWeapon(attackerId);
            var defenderItem = await GetEquippedArmor(defenderId);
            var attackSuccess = ResolveAttack(attacker, defender);
            
                fightId = _fightService.Create(new FightDto
                {
                    AttackerId = attacker.Id,
                    DefenderId = defender.Id,
                    AttackerItemId = attackerItem?.Id,
                    DefenderItemId = defenderItem?.Id,
                    Timestamp = DateTime.Now,
                    AttackSuccess = attackSuccess
                });
                attacker = _characterService.GetAsync(attackerId, withIncludes: false).Result;
                attacker.Money += 30;
                _characterService.Update(attacker).Wait();
                await uow.Commit();
            }
            return fightId;

        }

        public async Task<bool> AddMoneyToCharacter(Guid characterId, int value)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var character = await _characterService.GetAsync(characterId);
                character.Money += value;
                await _characterService.Update(character);
                await uow.Commit();
            }
            return true;
        }


        private bool ResolveAttack(CharacterDto attacker, CharacterDto defender)
        {
            return true;
        }
    }
}
