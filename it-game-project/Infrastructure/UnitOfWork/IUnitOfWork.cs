using System;
using System.Threading.Tasks;

namespace Game.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();

        void RegisterAction(Action action);
    }
}