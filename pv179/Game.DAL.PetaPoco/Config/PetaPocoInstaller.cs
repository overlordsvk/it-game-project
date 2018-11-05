﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncPoco;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Game.Infrastructure;
using Game.Infrastructure.PetaPoco;
using Game.Infrastructure.PetaPoco.UnitOfWork;
using Game.Infrastructure.Query;
using Game.Infrastructure.UnitOfWork;

namespace Game.DAL.PetaPoco.Config
{
    public class PetaPocoInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<IDatabase>>()
                    .Instance(() => new Database("Data source=(localdb)\\mssqllocaldb;Database=DemoEshopDatabaseSample;Trusted_Connection=True;MultipleActiveResultSets=true", "System.Data.SqlClient"))
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<PetaPocoUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(PetaPocoRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(PetaPocoQuery<>))
                    .LifestyleTransient()
            );
        }
    }
}