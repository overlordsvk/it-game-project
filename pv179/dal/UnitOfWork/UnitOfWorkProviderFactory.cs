using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.UnitOfWork
{
    public static class UnitOfWorkProviderFactory
    {
        public static IUnitOfWorkProvider Create()
        {
            return new UnitOfWorkProvider(() => new GameDbContext());
        }
    }
}
