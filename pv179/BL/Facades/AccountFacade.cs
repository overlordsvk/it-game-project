using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Facades.Common;
using Game.Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class AccountFacade : FacadeBase
    {
        public AccountFacade(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {

        }
    }
}
