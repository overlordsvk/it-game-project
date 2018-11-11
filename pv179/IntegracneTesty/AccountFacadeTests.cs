using System;
using System.Linq;
using BL.DTO;
using BL.Facades;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using Game.Infrastructure.UnitOfWork;
using IntegracneTesty.Config;
using IntegracneTesty.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegracneTesty
{
    [TestClass]
    public class AccountFacadeTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();
        private readonly AccountFacade accountFacade = Initializer.Container.Resolve<AccountFacade>();
        private readonly IRepository<Account> accountRepository = Initializer.Container.Resolve<IRepository<Account>>();






        [TestMethod]
        public async System.Threading.Tasks.Task GetAccountAccordingToEmailAsync()
        {
            AccountDto Peter;

            Peter = await accountFacade.GetAccountAccordingToEmailAsync(Konstanty.accountPeter.Email);

            Assert.AreEqual(Konstanty.guidAccountPeter, Peter.Id);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetAccountAccordingToUsernameAsync()
        {
            AccountDto Peter;

            Peter = await accountFacade.GetAccountAccordingToUsernameAsync(Konstanty.accountPeter.Username);

            Assert.AreEqual(Konstanty.guidAccountPeter, Peter.Id);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetAllAccountsAsyncReturnAll()
        {
            var result = await accountFacade.GetAllAccountsAsync();

            Assert.AreEqual(result.Items.ToList().Count, 3);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task LoginAccount()
        {
            var Ivan = await accountFacade.GetAccountAccordingToUsernameAsync(Konstanty.accountIvan.Username);
            var result = await accountFacade.Login(Konstanty.accountIvan.Username, Konstanty.accountIvan.Password);

            Assert.AreEqual(result.Id, Ivan.Id);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task RegisterAccount()
        {
            using (unitOfWorkProvider.Create())
            {
                AccountCreateDto accountToRegister = new AccountCreateDto
                {
                    Email = "Register@account.com",
                    Password = "trytoregister",
                    Username = "IamRobot",
                };
                var registered = await accountFacade.RegisterAccount(accountToRegister);
                var acc = await accountRepository.GetAsync(registered);
                Console.WriteLine(acc.Username);
                var result = await accountFacade.GetAccountAccordingToUsernameAsync(acc.Username);

                Assert.AreEqual(result.Id, registered);
            }
        }

    }

}
