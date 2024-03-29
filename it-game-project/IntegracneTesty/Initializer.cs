﻿using Castle.Windsor;
using Game.DAL.Entity;
using IntegracneTesty.Tests.Config;
using NUnit.Framework;
using System.Data.Entity;

namespace IntegracneTesty.Tests
{
    [SetUpFixture]
    public class Initializer
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer().Install(new IntegracneTestyInstaller());

        /// <summary>
        /// Initializes all Business Layer tests
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            Database.SetInitializer(new DropCreateDatabaseAlways<GameDbContext>());
        }
    }
}