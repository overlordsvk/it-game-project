using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using Game.DAL.Entity.Entities;
using Game.Infrastructure.Query;

namespace BL.QueryObject
{
    public class CharacterQueryObject : QueryObjectBase<CharacterDto, Character, CharacterFilterDto, IQuery<Character>>
    {
        public CharacterQueryObject(IMapper mapper, IQuery<Character> query) : base(mapper, query)
        {
        }

        protected override IQuery<Character> ApplyWhereClause(IQuery<Character> query, CharacterFilterDto filter)
        {
            throw new NotImplementedException();
        }
    }
}
