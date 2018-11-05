using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor.Installer;
using DAL.EntityFrameWork.Tests.Config;
using Game.DAL.Entity;
using NUnit.Framework;

namespace DAL.EntityFrameWork.Tests
{
    [SetUpFixture]
    public class Initializer
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer().Install(FromAssembly.This());

        /// <summary>
        /// Initializes all Business Layer tests
        /// </summary>
        [OneTimeSetUp]
        public void InitializeBusinessLayerTests()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            Database.SetInitializer(new DropCreateDatabaseAlways<GameDbContext>());
        }
    }
}
