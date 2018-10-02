using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public Task Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void RegisterAction(Action action)
        {
            throw new NotImplementedException();
        }
    }
}
