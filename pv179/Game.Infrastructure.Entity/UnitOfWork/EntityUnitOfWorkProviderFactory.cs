using Game.DAL.Entity;
using Game.Infrastructure.UnitOfWork;

namespace Game.Infrastructure.Entity.UnitOfWork
{
    public static class EntityUnitOfWorkProviderFactory
    {
        public static IUnitOfWorkProvider Create()
        {
            return new EntityUnitOfWorkProvider(() => new GameDbContext());
        }
    }
}
