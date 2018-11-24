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
using System.Runtime.InteropServices;
using BL.Services.Groups;
using System.Security.Cryptography;

namespace BL.Facades
{
    public class CharacterFacade : FacadeBase
    {
        private readonly IAccountService _accountService;
        private readonly ICharacterService _characterService;
        private readonly IGroupService _groupService;
        private readonly IItemService _itemService;
        private readonly IFightService _fightService;
        private readonly Random _random = new Random();

        public CharacterFacade(IUnitOfWorkProvider unitOfWorkProvider, ICharacterService characterService, IAccountService accountService, IGroupService groupService, IItemService itemService, IFightService fightService) : base(unitOfWorkProvider)
        {
            _accountService = accountService;
            _characterService = characterService;
            _groupService = groupService;
            _itemService = itemService;
            _fightService = fightService;
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

        public async Task<CharacterDto> GetCharacterAccordingToNameAsync(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _characterService.GetCharacterAccordingToNameAsync(name);
            }
        }

        /// <summary>
        /// Gets character according to ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Character by id</returns>
        public async Task<Guid> CreateCharacter(Guid AccountId, CharacterDto character)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                character.Id = AccountId;
                var acc = await _accountService.GetAsync(AccountId);
                acc.Character = character;
                acc.Roles += ", HasCharacter";
                await _accountService.Update(acc);
                await uow.Commit();
                return AccountId;
            }
        }

        public async Task<Guid> EditCharacter(Guid AccountId, CharacterDto character)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                character.Id = AccountId;
                var acc = await _accountService.GetAsync(AccountId);
                acc.Character = character;
                await _accountService.Update(acc);
                await uow.Commit();
                return AccountId;
            }
        }


        public async Task<bool> RemoveCharacter(Guid CharacterId)
        {
            using(var uow = UnitOfWorkProvider.Create())
            {
                var character = await _characterService.GetAsync(CharacterId, withIncludes: false);
                if (character == null)
                {
                    return false;
                }
                var acc = await _accountService.GetAsync(CharacterId);
                acc.Character = character;
                acc.Roles.Replace(", HasCharacter", "");
                _characterService.Delete(CharacterId);
                await uow.Commit();
                return true;
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
                var res = await _itemService.EquipItem(characterId, itemId);
                await uow.Commit();
                return res;
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
                var attackerArmor = await GetEquippedArmor(attackerId);
                var attackerWeapon = await GetEquippedWeapon(attackerId);
                var defenderArmor = await GetEquippedArmor(defenderId);
                var defenderWeapon = await GetEquippedWeapon(defenderId);
                var attackSuccess = ResolveAttack(attacker, attackerWeapon, attackerArmor, defender, defenderWeapon, defenderArmor);
                Guid fightId = _fightService.Create(new FightDto
                    {
                        Id = Guid.NewGuid(),
                        AttackerId = attacker.Id,
                        DefenderId = defender.Id,
                        AttackerArmorId = attackerArmor?.Id,
                        AttackerWeaponId = attackerWeapon?.Id,
                        DefenderArmorId = defenderArmor?.Id,
                        DefenderWeaponId = defenderWeapon?.Id,
                        Timestamp = DateTime.Now,
                        AttackSuccess = attackSuccess
                    });
                attacker.Money += 30;
                await _characterService.Update(attacker);
                await uow.Commit();
                return fightId;
            }
        }

        public async Task<bool> AddMoneyToCharacter(Guid characterId, int value)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var character = await _characterService.GetAsync(characterId);
                character.Money += value;
                await _characterService.Update(character);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> JoinGroup(Guid characterId, Guid groupId)
        {
            using(var uow = UnitOfWorkProvider.Create())
            {
                var character = await _characterService.GetAsync(characterId, withIncludes: false);
                var group = await _groupService.GetAsync(groupId);
                if (character == null || group == null)
                {
                    return false;
                }
                character.GroupId = groupId;
                await _characterService.Update(character);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> LeaveGroup(Guid characterId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var character = await _characterService.GetAsync(characterId, withIncludes: false);
                if (!character.GroupId.HasValue)
                {
                    return false;
                }
                if (character.IsGroupAdmin)
                {
                    character.IsGroupAdmin = false;
                }
                character.GroupId = null;
                await _characterService.Update(character);
                await uow.Commit();
                return true;
            }
        }


        private bool ResolveAttack(CharacterDto attacker, ItemDto attackerWeapon, ItemDto attackerArmor, CharacterDto defender, ItemDto defenderWeapon, ItemDto defenderArmor)
        {   
            var attackerHealth = attacker.Health * 10;
            var defenderHealth = defender.Health * 10;

            var attDamage = 10;
            var attDefense = 10;
            var attackerWeight = 0;

            var defDamage = 10;
            var defDefense = 10;
            var defenderWeight = 0;

            if (attackerWeapon != null)
            {
                attDamage += attackerWeapon.Attack;
                attDefense += attackerWeapon.Defense;
                attackerWeight += attackerWeapon.Weight;
            }
            if (attackerArmor != null)
            {
                attDamage += attackerArmor.Attack;
                attDefense += attackerArmor.Defense;
                attackerWeight += attackerArmor.Weight;
            }
            if (defenderWeapon != null)
            {
                defDamage += defenderWeapon.Attack;
                defDefense += defenderWeapon.Defense;
                defenderWeight += defenderWeapon.Weight;
            }
            if (defenderArmor != null)
            {
                defDamage += defenderArmor.Attack;
                defDefense += defenderArmor.Defense;
                defenderWeight += defenderArmor.Weight;
            }

            attDamage = ((attacker.Strength + attacker.Intelligence + attacker.Charisma) * attDamage) / 12;
            attDefense = ((attacker.Agility + attacker.Endurance + attacker.Perception) * attDefense) / 12;
            defDamage = ((defender.Strength + defender.Intelligence + defender.Charisma) * defDamage) / 12;
            defDefense = ((defender.Agility + defender.Endurance + defender.Perception) * defDefense) / 12;

            while (attackerHealth > 0 || defenderHealth > 0)
            {
                if (_random.Next(0,100) < (50 + attacker.Luck - attackerWeight))
                    defenderHealth -= attDamage * (1 - (defDefense / 1500));
                if (defenderHealth < 0)
                    return true;
                if (_random.Next(0,100) < (50 + defender.Luck - defenderWeight))
                    attackerHealth -= defDamage * (1 - (attDefense / 1500));
                if (attackerHealth < 0)
                    return false;
            }
            return false;
        }
    }
}
