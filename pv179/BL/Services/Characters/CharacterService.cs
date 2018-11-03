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
using Game.Infrastructure;
using Game.Infrastructure.Query;

namespace BL.Services.Characters
{
    public class CharacterService : CrudQueryServiceBase<Character, CharacterDto, CharacterFilterDto>, ICharacterService
    {
        public CharacterService(IMapper mapper, IRepository<Character> repository, QueryObjectBase<CharacterDto, Character, CharacterFilterDto, IQuery<Character>> query) : base(mapper, repository, query)
        {
        }

        protected override async Task<Character> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Character.Account), nameof(Character.Group), nameof(Character.Chats), nameof(Character.Items));
        }

        public async Task<QueryResultDto<CharacterDto, CharacterFilterDto>> ListCharactersAsync(CharacterFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
