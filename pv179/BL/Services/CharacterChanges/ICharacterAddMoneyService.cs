using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.CharacterChanges
{
    public interface ICharacterAddMoneyService
    {
        Task<bool> AddMoney(Guid characterId, int value);
    }
}
