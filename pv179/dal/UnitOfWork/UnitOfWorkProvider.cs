using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        protected readonly AsyncLocal<IUnitOfWork> UowLocalInstance
            = new AsyncLocal<IUnitOfWork>();

        private readonly Func<GameDbContext> dbContextFactory;

        public UnitOfWorkProvider(Func<GameDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public IUnitOfWork Create()
        {
            UowLocalInstance.Value = new UnitOfWork(dbContextFactory);
            return UowLocalInstance.Value;
        }

        public IUnitOfWork GetUnitOfWorkInstance()
        {
            return UowLocalInstance != null ? UowLocalInstance.Value : throw new InvalidOperationException("UoW not created");
        }

        public void Dispose()
        {
            UowLocalInstance.Value?.Dispose();
            UowLocalInstance.Value = null;
        }
    }
}
