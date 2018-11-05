using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using Game.Infrastructure.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace InfrastructureTests
{
    //[TestClass]
    //public class RepositoryTests
    //{
    //    private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

    //    private readonly IRepository<Account> _accountRepository = Initializer.Container.Resolve<IRepository<Account>>();
    //    private readonly IRepository<Character> _characterRepository = Initializer.Container.Resolve<IRepository<Character>>();
    //    private readonly IRepository<Item> _itemRepository = Initializer.Container.Resolve<IRepository<Item>>();

    //    private readonly int peterId = 1;
    //    private readonly int accCount = 3;
    //    private readonly string ivanCharacterName = "KingSlayer";


    //    [TestMethod]
    //    public async Task GetAccountAsync()
    //    {
    //        Account peter;

    //        using (unitOfWorkProvider.Create())
    //        {
    //            peter = await _accountRepository.GetAsync(peterId);
    //        }

    //        Console.WriteLine(peter.Username);
    //        Assert.AreEqual(peter.Id, peterId);
    //    }

    //    [TestMethod]
    //    public async Task GetAllAccount()
    //    {
    //        int accountsCount;

    //        using (unitOfWorkProvider.Create())
    //        {
    //            var accounts = await _accountRepository.GetAllAsync();
    //            accountsCount = accounts.Count;
    //        }

    //        Assert.AreEqual(accountsCount, accCount);
    //    }

    //    [TestMethod]
    //    public async Task GetAccountCharacterName()
    //    {
    //        Account ivan;

    //        using (unitOfWorkProvider.Create())
    //        {
    //            ivan = await _accountRepository.GetAsync(3);

    //            Assert.AreEqual(ivan.Character.Name, ivanCharacterName);
    //        }

            
    //    }
            
    //}
}
