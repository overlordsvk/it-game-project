using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.Facades.Common;
using BL.Services.Accounts;
using BL.Services.Characters;
using BL.Services.Fights;
using BL.Services.Groups;
using BL.Services.Items;
using Game.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<Guid> CreateCharacter(Guid accountId, CharacterDto character)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                character.Id = accountId;
                var account = await _accountService.GetAsync(accountId);
                character.Health = character.Endurance * 100;
                account.Character = character;
                account.Roles += ",HasCharacter";
                await _accountService.Update(account);
                await uow.Commit();
                return accountId;
            }
        }

        public async Task<Guid> EditCharacter(CharacterDto character)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                await _characterService.Update(character);
                await uow.Commit();
                return character.Id;
            }
        }

        public async Task<bool> RemoveCharacter(Guid CharacterId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var character = await _characterService.GetAsync(CharacterId, withIncludes: false);
                if (character == null)
                {
                    return false;
                }
                var acc = await _accountService.GetAsync(CharacterId);
                acc.Roles = acc.Roles.Replace(",HasCharacter", "");
                await _accountService.Update(acc);
                _characterService.Delete(CharacterId);
                await uow.Commit();
                return true;
            }
        }

        /// <summary>
        /// Gets inventory of character
        /// </summary>
        /// <returns>all customers</returns>
        public async Task<QueryResultDto<ItemDto, ItemFilterDto>> GetItemsByFilterAsync(ItemFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.ListItemsAsync(filter);
            }
        }

        public async Task<ItemDto> GetItem(Guid itemId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.GetAsync(itemId);
            }
        }

        public async Task<ItemDto> GetEquippedWeapon(Guid characterId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.GetEquippedWeapon(characterId);
            }
        }

        public async Task<ItemDto> GetEquippedArmor(Guid characterId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.GetEquippedArmor(characterId);
            }
        }

        public async Task<bool> SellItem(Guid itemId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var item = await _itemService.GetAsync(itemId);
                if (item == null)
                    return false;
                var ownerId = item.OwnerId;
                if (!ownerId.HasValue)
                    return false;
                var owner = await _characterService.GetAsync(ownerId.Value);
                owner.Money += item.Price;
                item.OwnerId = null;
                await _itemService.Update(item);
                await _characterService.Update(owner);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> EquipItemAsync(Guid characterId, Guid itemId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var res = await _itemService.EquipItem(characterId, itemId);
                await uow.Commit();
                return res;
            }
        }

        public async Task<bool> BuyItemAsync(Guid characterId, ItemDto item)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var character = await _characterService.GetAsync(characterId);
                if (character == null)
                {
                    return false;
                }

                if (character.Money < item.Price)
                    return false;

                character.Money -= item.Price;
                item.OwnerId = characterId;
                item.Price /= 2;
                _itemService.Create(item);
                await _characterService.Update(character);
                await uow.Commit();
                return true;
            }
        }

        public ICollection<ItemDto> GetItemsForShop()
        {
            var items = new List<ItemDto>();
            for (int i = 0; i < 5; i++)
            {
                items.Add(_itemService.GetNewItem());
            }
            return items;
        }

        public async Task<Guid> GiveItemAsync(Guid characterId, ItemDto item = null)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var character = await _characterService.GetAsync(characterId);
                if (character == null)
                    return Guid.Empty;
                if (item == null)
                    item = _itemService.GetNewItem();
                item.OwnerId = characterId;
                var itemId = _itemService.Create(item);
                character.Items.Add(item);
                await _characterService.Update(character);
                await uow.Commit();
                return itemId;
            }
        }

        public async Task<IEnumerable<CharacterDto>> GetCharactersToFight(CharacterDto character, int count)
        {
            using (UnitOfWorkProvider.Create())
            {
                var characters = await _characterService.ListCharactersAsync(new CharacterFilterDto { SortCriteria = nameof(character.Score) });
                var lessScore = characters.Items.TakeWhile(x => x.Name != character.Name);
                var moreScore = characters.Items.SkipWhile(x => x.Name != character.Name);
                moreScore = moreScore.Skip(1);
                var possibleCharacters = new List<CharacterDto>();

                if (lessScore.Count() < count / 2)
                {
                    possibleCharacters.AddRange(lessScore);
                }
                else
                {
                    possibleCharacters.AddRange(lessScore.Skip(lessScore.Count() - count / 2));
                }

                if (moreScore.Count() < count - possibleCharacters.Count())
                {
                    var i = possibleCharacters.Count();
                    possibleCharacters.AddRange(moreScore);
                }
                else
                {
                    possibleCharacters.AddRange(moreScore.Take(count - possibleCharacters.Count()));
                }
                return possibleCharacters;
            }
        }

        public async Task<FightDto> GetFight(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _fightService.GetAsync(id);
            }
        }

        public async Task<QueryResultDto<FightDto, FightFilterDto>> GetFightsHistory(FightFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _fightService.ListFightsAsync(filter);
            }
        }

        public async Task<(Guid, ICollection<(int, int, int, int, int, int, int)>)> Attack(Guid attackerId, Guid defenderId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var attacker = await _characterService.GetAsync(attackerId);
                var defender = await _characterService.GetAsync(defenderId);

                if (attacker == null)
                {
                    return (Guid.Empty, null);
                }
                if (defender == null)
                {
                    return (Guid.Empty, null);
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
                    AttackSuccess = attackSuccess.Item1
                });
                if (attackSuccess.Item1)
                {
                    attacker.Score += 30;
                    attacker.Money += 30 + (_random.Next(1, 7 * attacker.Luck));
                }
                else
                {
                    attacker.Score -= 17;
                }
                await _characterService.Update(attacker);
                await uow.Commit();
                return (fightId, attackSuccess.Item2);
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
            using (var uow = UnitOfWorkProvider.Create())
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

        private (bool, ICollection<(int Order, int HPAtt, int HPDef, int DmgAtt, int DmgDef, int LuckAtt, int LuckDef)>) ResolveAttack(CharacterDto attacker, ItemDto attackerWeapon, ItemDto attackerArmor, CharacterDto defender, ItemDto defenderWeapon, ItemDto defenderArmor)
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

            var fight = new List<(int Order, int HPAtt, int HPDef, int DmgAtt, int DmgDef, int LuckAtt, int LuckDef)>();
            int order = 0;
            fight.Add((order, attackerHealth, defenderHealth,0,0,0,0));
            var attackerWin = false;
            while (attackerHealth > 0 || defenderHealth > 0)
            {
                order++;
                var luckAtt = _random.Next(1, 100);
                var damageAtt = 0;
                var damageDef = 0;
                if (luckAtt < (50 + attacker.Luck - attackerWeight))
                {
                    damageAtt = attDamage * (1 - (defDefense / 1500)) + _random.Next(0, attacker.Luck);
                    defenderHealth -= damageAtt;
                }

                if (defenderHealth <= 0)
                {
                    fight.Add((order, attackerHealth, 0, damageAtt, 0, 100 - luckAtt, 0));
                    attackerWin = true;
                    break;
                }

                var luckDef = _random.Next(1, 100);
                if (_random.Next(0, 100) < (50 + defender.Luck - defenderWeight))
                {
                    damageDef = defDamage * (1 - (attDefense / 1500)) + _random.Next(0, defender.Luck);
                    attackerHealth -= damageDef;
                }
                if (attackerHealth <= 0)
                {
                    attackerWin = false;
                    fight.Add((order, 0, defenderHealth, damageAtt, damageDef, 100 - luckAtt, 100 - luckDef));
                    break;
                }
                fight.Add((order, attackerHealth, defenderHealth, damageAtt, damageDef, 100 - luckAtt, 100 - luckDef));
            }
            return (attackerWin, fight);
        }
    }
}