using BL.DTO;
using BL.DTO.Filters;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Models
{
    public class CharactersController : ApiController
    {
        public CharacterFacade CharacterFacade { get; set; }

        /// <summary>
        /// Example URL call: http://localhost:62677/api/Characters/List?scoreMin=5&scoreMax=15&asc=true&sort=name&pageSize=10&pageNumber=1&groupId=e9c469c0-60e0-437e-ad8f-c9a742e551b0
        /// Returns KingSlayer (according to initial DB)
        /// <summary>
        /// <param name="sort">Name of the attribute (e.g. "name", "score", ...) to sort according to</param>
        /// <param name="asc">Sort collection in ascending manner</param>
        /// <param name="groupId">Guid of given group</param>
        /// <param name="name">Name of character to get</param>
        /// <param name="scoreMin">Min score of character</param>
        /// <param name="scoreMax">Max score of character</param>
        /// <param name="pageSize">How many results per req.</param>
        /// <param name="pageNumber">Page number</param>
        /// <returns>Collection of products, satisfying given query parameters.</returns>
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
                filter.GroupId = Guid.Parse(groupId);

            var characters = (await CharacterFacade.GetCharactersByFilterAsync(filter)).Items;
            return characters;
        }

        /// <summary>
        /// Example URL call: http://localhost:62677/api/Characters/?name=King SLAYER&id=b49c2b2f-99b0-4f21-8655-70b0d2c7c5d2
        /// Change name of character
        /// <summary>
        /// <param name="id">Guid of character</param>
        /// <param name="name">New name</param>
        /// <returns>New name of character with id {id} is {name}</returns>
        [HttpPatch]
        public async Task<string> Patch(Guid id, string name)
        {
            var character = await CharacterFacade.GetCharacterById(id);
            if (character == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            character.Name = name;
            await CharacterFacade.EditCharacter(character);
            return $"New name of character with id {character.Id} is {character.Name}";
        }
    }
}