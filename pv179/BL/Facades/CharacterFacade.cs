using BL.Facades.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class CharacterFacade : FacadeBase
    {
        private readonly IAccountService _accountService;

        public CharacterFacade(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {

        }
    }
}
