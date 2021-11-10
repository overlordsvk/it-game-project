using AutoMapper;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.QueryObject;
using BL.Services.Common;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using Game.Infrastructure.Query;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services.Characters
{
    public class CharacterService : CrudQueryServiceBase<Character, CharacterDto, CharacterFilterDto>, ICharacterService
    {
        public CharacterService(IMapper mapper, IRepository<Character> repository, QueryObjectBase<CharacterDto, Character, CharacterFilterDto, IQuery<Character>> query) : base(mapper, repository, query)
        {
        }

        protected override async Task<Character> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Character.Account), nameof(Character.Group), nameof(Character.Items), nameof(Character.ReceiverChats), nameof(Character.SenderChats));
        }

        public async Task<QueryResultDto<CharacterDto, CharacterFilterDto>> ListCharactersAsync(CharacterFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        public async Task<CharacterDto> GetCharacterAccordingToNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new CharacterFilterDto { Name = name });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<bool> AddMoney(Guid characterId, int value)
        {
            var character = await GetAsync(characterId, withIncludes: false);

            if (character == null || character.Money - value < 0)
            {
                return false;
            }
            character.Money += value;
            await Update(character);
            return true;
        }
    }
}