using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWorkProvider : IDisposable
    {

        IUnitOfWork Create();

        IUnitOfWork GetUnitOfWorkInstance();
    }
}
