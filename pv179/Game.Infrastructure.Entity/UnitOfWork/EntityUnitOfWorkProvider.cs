using Game.Infrastructure.UnitOfWork;
using System;
using System.Data.Entity;

namespace Game.Infrastructure.Entity.UnitOfWork
{
    public class EntityUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        private readonly Func<DbContext> dbContextFactory;

        public EntityUnitOfWorkProvider(Func<DbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public override IUnitOfWork Create()
        {
            UowLocalInstance.Value = new EntityUnitOfWork(dbContextFactory);
            return UowLocalInstance.Value;
        }
    }
}
