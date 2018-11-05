using System;
using System.Threading.Tasks;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using Game.Infrastructure.Entity.UnitOfWork;
using Game.Infrastructure.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DAL.EntityFrameWork.Tests
{
    [TestClass]
    public class EntityRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        private readonly IRepository<Account> accountRepository = Initializer.Container.Resolve<IRepository<Account>>();
        private readonly IRepository<Character> characterRepository = Initializer.Container.Resolve<IRepository<Character>>();

        private readonly Character characterSlayer = new Character
        {
            Name = "KingSlayer",
            Money = 666,
            Health = 98,
            Score = 12,
            Strength = 5,
            Perception = 8,
            Endurance = 2,
            Charisma = 8,
            Intelligence = 1,
            Agility = 5,
            Luck = 9
        };

        private readonly Account accountIvan = new Account
        {
            Username = "Ivan",
            Email = "navi@ivan.com",
            Password = "IvanJeBoh",
            IsAdmin = false,
           
        };

        private readonly Account accountJozo = new Account
        {
            Username = "Jozo",
            Email = "bob@pokec.sk",
            Password = "dota4life",
            IsAdmin = false,
        };

        [TestMethod]
        public async Task GetAccountAsync()
        {
            Account Ivan;

            using (unitOfWorkProvider.Create())
            {
                Ivan = await accountRepository.GetAsync(3);
            }

            Assert.AreEqual(Ivan.Email, accountIvan.Email);
        }

        [TestMethod]
        public async Task GetAccountWithIncludesAsync()
        {
            Account Ivan;

            using (unitOfWorkProvider.Create())
            {
                Ivan = await accountRepository.GetAsync(3, nameof(Character));
            }

            Assert.AreEqual(Ivan.Character.Name, characterSlayer.Name);
        }

        [TestMethod]
        public async Task GetCharacterAsync()
        {
            Character slayer;

            using (unitOfWorkProvider.Create())
            {
                slayer = await characterRepository.GetAsync(3);
                Console.WriteLine(slayer.Name);
            }

            Assert.AreEqual(slayer.Name, characterSlayer.Name);
        }

        [TestMethod]
        public async Task CreateAccount()
        {
            Account jozo;

            using (var uow = unitOfWorkProvider.Create())
            {
                accountRepository.Create(accountJozo);
                await uow.Commit();
                jozo = await accountRepository.GetAsync(4);
            }
            Assert.AreEqual(jozo.Username, accountJozo.Username);
        }

        [TestMethod]
        public async Task UpdateAccount()
        {
            var jozo = accountJozo;
            var mail = "jozo@azet.sk";
            Account res;

            using (var uow = unitOfWorkProvider.Create())
            {
                jozo = await accountRepository.GetAsync(4);
                jozo.Email = mail;
                accountRepository.Update(jozo);
                await uow.Commit();
                res = await accountRepository.GetAsync(4);
            }
            Assert.AreEqual(res.Email, mail);
        }
    }
}
