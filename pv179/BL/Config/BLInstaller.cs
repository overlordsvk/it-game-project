using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.QueryObject;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BL.Config
{
    public class BLInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn(typeof(QueryObjectBase<,,,>))
                    .WithServiceBase()
                    .LifestyleTransient(),

//                Classes.FromThisAssembly()
//                    .BasedOn<ServiceBase>()
//                    .WithServiceDefaultInterfaces()
//                    .LifestyleTransient(),
//
//                Classes.FromThisAssembly()
//                    .BasedOn<IService>()
//                    .WithService.FromInterface()
//                    .LifestyleSingleton(),
//
//                Classes.FromThisAssembly()
//                    .BasedOn<FacadeBase>()
//                   .LifestyleTransient(),

                Component.For<IMapper>()
                    .Instance(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)))
                    .LifestyleSingleton()
            );
        }
    }
}
