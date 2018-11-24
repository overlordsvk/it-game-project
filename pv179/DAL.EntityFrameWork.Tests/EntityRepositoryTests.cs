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

        private static readonly Guid guid1 = Guid.Parse("b49c2b2f-99b0-4f21-8655-70b0d2c7c5d2");
        private static readonly Guid guid2 = Guid.Parse("d69fe626-bb15-4ec6-a778-1d3c91ad213b");
        private static readonly Guid guid3 = Guid.Parse("139892fc-4a88-4636-aa0e-1975adf2ee11");

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
            Luck = 9,
            Id = guid2
        };

        private readonly Account accountIvan = new Account
        {
            Username = "Ivan",
            Email = "jozo@azet.sk",
            PasswordHash = "IvanJeBoh",
            PasswordSalt = "IvanJeBoh",
            Roles = "",
            Id = guid1
        };

        private readonly Account accountJozo = new Account
        {
            Username = "Jozo",
            Email = "bob@pokec.sk",
            PasswordHash = "dota4life",
            PasswordSalt = "dota4life",
            Roles = "",
            Id = guid3
        };

        [TestMethod]
        public async Task GetAccountAsync()
        {
            Account Ivan;

            using (unitOfWorkProvider.Create())
            {
                Ivan = await accountRepository.GetAsync(guid1);
            }

            Assert.AreEqual(Ivan.Email, accountIvan.Email);
        }

        [TestMethod]
        public async Task GetAccountWithIncludesAsync()
        {
            Account Ivan;

            using (unitOfWorkProvider.Create())
            {
                Ivan = await accountRepository.GetAsync(guid1, nameof(Character));
            }

            Assert.AreEqual(Ivan.Character.Name, characterSlayer.Name);
        }

        [TestMethod]
        public async Task GetCharacterAsync()
        {
            Character slayer;

            using (unitOfWorkProvider.Create())
            {
                slayer = await characterRepository.GetAsync(guid1);
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
                var Id = accountRepository.Create(accountJozo);
                await uow.Commit();
                jozo = await accountRepository.GetAsync(Id);
            }
            Assert.AreEqual(jozo.Username, accountJozo.Username);
        }

        [TestMethod]
        public async Task UpdateAccount()
        {
            var ivan = accountIvan;
            var mail = "jozo@azet.sk";
            Account res;

            using (var uow = unitOfWorkProvider.Create())
            {
                ivan = await accountRepository.GetAsync(guid1);
                ivan.Email = mail;
                accountRepository.Update(ivan);
                await uow.Commit();
                res = await accountRepository.GetAsync(guid1);
            }
            Assert.AreEqual(res.Email, mail);
        }
    }
}
