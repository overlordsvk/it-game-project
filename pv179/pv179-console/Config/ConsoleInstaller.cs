using BL.Config;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Game.DAL.Entity.Config;
using System;
using System.Linq;

namespace pv179_console.Config
{
    public class ConsoleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new EntityInstaller().Install(container, store);
            new BLInstaller().Install(container, store);
        }
    }
}
