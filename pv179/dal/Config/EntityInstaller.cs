using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Game.Infrastructure;
using Game.Infrastructure.Entity;
using Game.Infrastructure.Entity.Repository;
using Game.Infrastructure.Entity.UnitOfWork;
using Game.Infrastructure.Query;
using Game.Infrastructure.UnitOfWork;
using System;
using System.Data.Entity;

namespace Game.DAL.Entity.Config
{
    public class EntityInstaller : IWindsorInstaller
    {
        internal const string AzureDbConnection = "Server=tcp:pv179-mol-bal.database.windows.net,1433;Initial Catalog=PV179DB;Persist Security Info=False;User ID=xbaltaz;Password=LinearneMB102;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
        internal const string LocalDbConnection = "Data source=(localdb)\\mssqllocaldb;Database=GameDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(() => new GameDbContext())
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EntityUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EntityRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EntityQuery<>))
                    .LifestyleTransient()
            );
        }
    }
}