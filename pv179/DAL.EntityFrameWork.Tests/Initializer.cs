using Castle.Windsor;
using DAL.EntityFrameWork.Tests.Config;
using Game.DAL.Entity;
using NUnit.Framework;
using System.Data.Entity;

namespace DAL.EntityFrameWork.Tests
{
    [SetUpFixture]
    public class Initializer
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer().Install(new EntityFrameworkInstaller());

        /// <summary>
        /// Initializes all Business Layer tests
        /// </summary>
        [OneTimeSetUp]
        public void Initialize()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            Database.SetInitializer(new DropCreateDatabaseAlways<GameDbContext>());
        }
    }
}