using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncPoco;
using Game.Infrastructure.UnitOfWork;

namespace Game.Infrastructure.PetaPoco.UnitOfWork
{
    public class PetaPocoUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        private readonly Func<IDatabase> dbFactory;

        public PetaPocoUnitOfWorkProvider(Func<IDatabase> dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public override IUnitOfWork Create()
        {
            UowLocalInstance.Value = new PetaPocoUnitOfWork(dbFactory);
            return UowLocalInstance.Value;
        }
    }
}
