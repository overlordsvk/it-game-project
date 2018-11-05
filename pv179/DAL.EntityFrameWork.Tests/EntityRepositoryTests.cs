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
            Id = 200,
            Username = "Ivan",
            Email = "navi@ivan.com",
            Password = "IvanJeBoh",
            IsAdmin = false,
            //Character = characterSlayer
        };

        [TestMethod]
        public async Task GetAccountAsync()
        {
            Account Ivan;

            using (unitOfWorkProvider)
            {
                Ivan = await accountRepository.GetAsync(200);
            }

            Assert.AreEqual(Ivan.Email, accountIvan.Email);
        }
    }
}
