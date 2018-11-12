using System;
using System.Threading.Tasks;
using BL.Facades;
using IntegracneTesty.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace IntegracneTesty
{
    [TestFixture]
    public class CharacterFacadeTests
    {
        private readonly CharacterFacade groupFacade = Initializer.Container.Resolve<CharacterFacade>();

        [Test]
        public async Task GetAllC()
        {
            var result = await groupFacade.GetAllCharactersAsync();
            //result.
        }
    }
}
