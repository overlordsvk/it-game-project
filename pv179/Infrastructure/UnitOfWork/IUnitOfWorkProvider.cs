using System;

namespace Game.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkProvider : IDisposable
    {
        IUnitOfWork Create();

        IUnitOfWork GetUnitOfWorkInstance();
    }
}