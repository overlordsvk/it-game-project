using AutoMapper;
using BL.Services.Common;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.CharacterChanges
{
    public class CharacterAddMoneyService : ServiceBase, ICharacterAddMoneyService
    {
        private readonly IRepository<Character> _characterRepository;

        public CharacterAddMoneyService(IMapper mapper, IRepository<Character> characterRepository) : base(mapper)
        {
            this._characterRepository = characterRepository;
        }

        public async Task<bool> AddMoney(Guid characterId, int value)
        {
            var character = await _characterRepository.GetAsync(characterId);

            if (character == null || character.Money - value < 0)
            {
                return false;
            }
            character.Money += value;
            _characterRepository.Update(character);
            return true;
        }
    }
}
