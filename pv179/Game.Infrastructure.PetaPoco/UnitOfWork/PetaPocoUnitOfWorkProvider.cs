using AsyncPoco;
using Game.Infrastructure.UnitOfWork;
using System;

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