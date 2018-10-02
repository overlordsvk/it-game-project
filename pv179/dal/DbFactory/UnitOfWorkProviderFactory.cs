using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.DbFactory
{
    public static class UnitOfWorkProviderFactory
    {
        public static GameDbContext DbContext()
        {
            return new GameDbContext();
        }
    }
}
