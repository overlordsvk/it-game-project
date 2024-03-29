﻿using AutoMapper;
using BL.Facades.Common;
using BL.QueryObject;
using BL.Services.Common;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Game.DAL.Entity.Config;

namespace BL.Config
{
    public class BLInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new EntityInstaller().Install(container, store);
            //new PetaPocoInstaller().Install(container, store);

            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn(typeof(QueryObjectBase<,,,>))
                    .WithServiceBase()
                    .LifestyleTransient(),

                Classes.FromThisAssembly()
                    .BasedOn<ServiceBase>()
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient(),

                Classes.FromThisAssembly()
                    .BasedOn<FacadeBase>()
                   .LifestyleTransient(),

                Component.For<IMapper>()
                    .Instance(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)))
                    .LifestyleSingleton()
            );
        }
    }
}