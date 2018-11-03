using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Filters;
using BL.Services.Common;

namespace BL.Services.Characters
{
    public class CharacterService : CrudQueryServiceBase<Character, CharacterDto, CharacterFilterDto>, ICharacterService
    {

    }
}
