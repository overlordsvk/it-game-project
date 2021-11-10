using BL.DTO;
using BL.Facades;
using IntegracneTesty.Config;
using IntegracneTesty.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace IntegracneTesty
{
    [TestClass]
    public class AccountFacadeTests
    {
        private readonly AccountFacade accountFacade = Initializer.Container.Resolve<AccountFacade>();

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
        public async Task RegisterAccount()
        {
            AccountCreateDto accountToRegister = new AccountCreateDto
            {
                Email = "Register@account.com",
                Password = "trytoregister",
                Username = "IamRobot",
            };
            var registered = await accountFacade.RegisterAccount(accountToRegister);
            var result = accountFacade.GetAccountAccordingToEmailAsync(accountToRegister.Email).Result;
            Assert.AreEqual(result.Id, registered);
        }

        [TestMethod]
        public async Task RemoveAccount()
        {
            AccountCreateDto accountToRegister = new AccountCreateDto
            {
                Email = "Remove@account.com",
                Password = "trytoremove",
                Username = "IamRemove",
            };
            var registered = await accountFacade.RegisterAccount(accountToRegister);
            var afterReg = await accountFacade.GetAccountAccordingToEmailAsync(accountToRegister.Email);
            var result = await accountFacade.RemoveAccountAsync(registered);
            var res = await accountFacade.GetAccountAccordingToEmailAsync(accountToRegister.Email);

            Assert.AreEqual(result, true);
            Assert.AreEqual(afterReg.Username, accountToRegister.Username);
            Assert.AreEqual(res, null);
        }
    }
}