using BL.Config;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace pv179_console.Config
{
    public class ConsoleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new BLInstaller().Install(container, store);
        }
    }
}