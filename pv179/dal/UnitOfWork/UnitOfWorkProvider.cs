using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dal.UnitOfWork
{/*
    public class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        protected readonly AsyncLocal<IUnitOfWork> UowLocalInstance = new AsyncLocal<IUnitOfWork>();

        private readonly Func<GameDbContext> dbContextFactory;
        public UnitOfWorkProvider(Func<GameDbContext> DbContextFactory)
        {
            dbContextFactory = DbContextFactory;
        }

        public IUnitOfWork Create()
        {
            UowLocalInstance.Value = new UnitOfWork(dbContextFactory);
            return UowLocalInstance.Value;
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IUnitOfWork GetUnitOfWorkInstance()
        {
            throw new NotImplementedException();
        }
    }*/
}
