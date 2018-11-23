using BL.DTO;
using BL.DTO.Filters;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Models
{
    public class CharactersController : ApiController
    {
        public CharacterFacade CharacterFacade { get; set; }

        [HttpGet, Route("api/Characters/List")]
        public async Task<IEnumerable<CharacterDto>> List(string sort = null, bool asc = true, string name = null, int scoreMin = int.MinValue, int scoreMax = int.MaxValue, int pageSize = 50, int pageNumber = 1)
        {
            var filter = new CharacterFilterDto
            {
                //GroupId = groupId.Value,
                Name = name,
                PageSize = pageSize,
                RequestedPageNumber = pageNumber,
                ScoreMin = scoreMin,
                ScoreMax = scoreMax,
                SortAscending = asc,
                SortCriteria = sort
            };
            var characters = (await CharacterFacade.GetCharactersByFilterAsync(filter)).Items;
            foreach (var character in characters)
            {
                character.Id = Guid.Empty;
            }
            return characters;
        }
    }
}
