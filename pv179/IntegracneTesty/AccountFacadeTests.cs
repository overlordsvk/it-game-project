using System;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Facades;
using BL.Services.Accounts;
using BL.Services.Common;
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
        private readonly IAccountService accountService = Initializer.Container.Resolve<IAccountService>();






        [TestMethod]
        public async Task GetAccountAccordingToEmailAsync()
        {
            AccountDto Peter;

            Peter = await accountFacade.GetAccountAccordingToEmailAsync(Konstanty.accountPeter.Email);

            Assert.AreEqual(Konstanty.guidAccountPeter, Peter.Id);

        }

        [TestMethod]
        public async Task GetAccountAccordingToUsernameAsync()
        {
            AccountDto Peter;

            Peter = await accountFacade.GetAccountAccordingToUsernameAsync(Konstanty.accountPeter.Username);

            Assert.AreEqual(Konstanty.guidAccountPeter, Peter.Id);

        }

        [TestMethod]
        public async Task GetAllAccountsAsyncReturnAll()
        {
            var result = await accountFacade.GetAllAccountsAsync();

            Assert.AreEqual(result.Items.ToList().Count, 3);

        }

        [TestMethod]
        public async Task LoginAccount()
        {
            var Ivan = await accountFacade.GetAccountAccordingToUsernameAsync(Konstanty.accountIvan.Username);
            var result = await accountFacade.Login(Konstanty.accountIvan.Username, Konstanty.accountIvan.Password);

            Assert.AreEqual(result.Id, Ivan.Id);

        }

        [TestMethod]
        public async Task RegisterAccount()
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                AccountCreateDto accountToRegister = new AccountCreateDto
                {
                    Email = "Register@account.com",
                    Password = "trytoregister",
                    Username = "IamRobot",
                };
                var registered = await accountFacade.RegisterAccount(accountToRegister);
                var accc = await accountRepository.GetAsync(registered);
                var r = accountService.GetAccountAccordingToEmailAsync(accountToRegister.Email).Result;
                //Console.WriteLine("-" + r.Username);
                //var accounts = Initializer.Container.Resolve<IRepository<Account>>().GetAllAsync().Result;
                //var result = accountFacade.GetAccountAccordingToEmailAsync(accountToRegister.Email).Result;
                //Console.WriteLine("\nAccounts: ");
                //foreach (var acc in accounts)
                //{
                //    Console.WriteLine($"{acc.Id} \t  {acc.Username}  \t  {acc.Email}  \t \t Character:   {acc.Character?.Name}");
                //}
                Assert.AreEqual(r.Id, registered); /// POZOR NEJDE PRI RESULTE
            }
        }

        [TestMethod]
        public async Task RemoveAccount()
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

                var r = await accountService.GetAccountAccordingToEmailAsync(accountToRegister.Email);
                Console.WriteLine("-" + r.Username);
                var result = await accountFacade.RemoveAccountAsync(r.Id);

                Assert.AreEqual(r.Id, result); /// POZOR NEJDE PRI RESULTE
            }

        }

    }

}
