using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Game.DAL.Entity.Entities;
using Infrastructure.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace InfrastructureTests
{
    [TestClass]
    public class RepositoryTests
    {
        private readonly IRepository<Account> _accountRepository = new Repository<Account>(Initializer.Provider);
        private readonly IRepository<Character> _characterRepository = new Repository<Character>(Initializer.Provider);
        private readonly IRepository<Item> _itemRepository = new Repository<Item>(Initializer.Provider);

        private readonly int peterId = 1;
        private readonly int accCount = 3;
        private readonly string ivanCharacterName = "KingSlayer";


        [TestMethod]
        public async Task GetAccountAsync()
        {
            Account peter;

            using (Initializer.Provider.Create())
            {
                peter = await _accountRepository.GetAsync(peterId);
            }

            Console.WriteLine(peter.Username);
            Assert.AreEqual(peter.Id, peterId);
        }

        [TestMethod]
        public async Task GetAllAccount()
        {
            int accountsCount;

            using (Initializer.Provider.Create())
            {
                var accounts = await _accountRepository.GetAllAsync();
                accountsCount = accounts.Count;
            }

            Assert.AreEqual(accountsCount, accCount);
        }

        [TestMethod]
        public async Task GetAccountCharacterName()
        {
            Account ivan;

            using (Initializer.Provider.Create())
            {
                ivan = await _accountRepository.GetAsync(3);

                Assert.AreEqual(ivan.Character.Name, ivanCharacterName);
            }

            
        }
            
    }
}
