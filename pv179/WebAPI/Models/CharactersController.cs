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

        [HttpGet]
        public async Task<IEnumerable<CharacterDto>> List(string sort = null, 
                                                        bool asc = true, 
                                                        string groupId = null, 
                                                        string name = null, 
                                                        int scoreMin = int.MinValue, 
                                                        int scoreMax = int.MaxValue, 
                                                        int pageSize = 50, 
                                                        int pageNumber = 1)
        {
            var filter = new CharacterFilterDto
            {
                Name = name,
                PageSize = pageSize,
                RequestedPageNumber = pageNumber,
                ScoreMin = scoreMin,
                ScoreMax = scoreMax,
                SortAscending = asc,
                SortCriteria = sort
            };
            if (groupId != null)
            { 
                filter.GroupId = Guid.Parse(groupId);
            }
            var characters = (await CharacterFacade.GetCharactersByFilterAsync(filter)).Items;
            return characters;
        }
    }
}
