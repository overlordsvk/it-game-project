using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.DAL.Entity.Entities;
using Game.Infrastructure.Query;
using Game.Infrastructure.Query.Predicates;
using Game.Infrastructure.Query.Predicates.Operators;
using Game.Infrastructure.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DAL.EntityFrameWork.Tests
{
    [TestClass]
    public class EntityQueryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();
        private static readonly Guid guid = Guid.Parse("fd4e4768-037f-4ed9-8520-dd1e5a38816c");


        private readonly Account accountIvan = new Account
        {
            Id = Guid.Parse("b49c2b2f-99b0-4f21-8655-70b0d2c7c5d2"),
            Username = "Ivan",
            Email = "navi@ivan.com",
            Password = "IvanJeBoh",
            IsAdmin = false,

        };

        [TestMethod]
        public async Task QueryWithSimplePredicate()
        {
            QueryResult<Account> acctualResult;
            var accountQuery = Initializer.Container.Resolve<IQuery<Account>>();

            var expecterResult = new QueryResult<Account>(new List<Account>{accountIvan}, 1);
            var predicate = new SimplePredicate(nameof(Account.Email), ValueComparingOperator.Equal, accountIvan.Email);

            using (unitOfWorkProvider.Create())
            {
                acctualResult = await accountQuery.Where(predicate).ExecuteAsync();
            }

            Assert.AreEqual(expecterResult, acctualResult);
        }


        [TestMethod]
        public async Task QueryWithSimplePredicateWithStringSearch()
        {
            QueryResult<Account> acctualResult;
            var accountQuery = Initializer.Container.Resolve<IQuery<Account>>();

            var expecterResult = new QueryResult<Account>(new List<Account> { accountIvan }, 1);
            var predicate = new SimplePredicate(nameof(Account.Email), ValueComparingOperator.StringContains, accountIvan.Email);

            using (unitOfWorkProvider.Create())
            {
                acctualResult = await accountQuery.Where(predicate).ExecuteAsync();
            }

            Assert.AreEqual(expecterResult, acctualResult);
        }

        [TestMethod]
        public async Task QueryWithComplexPredicate()
        {
            QueryResult<Account> acctualResult;
            var accountQuery = Initializer.Container.Resolve<IQuery<Account>>();

            var expecterResult = new QueryResult<Account>(new List<Account> { accountIvan }, 1);
            var predicate = new CompositePredicate(
                new List<IPredicate>{
                    new SimplePredicate(nameof(Account.Email),
                        ValueComparingOperator.StringContains,
                        ".com"),
                    new SimplePredicate(nameof(Account.Email), 
                        ValueComparingOperator.StringContains, 
                        "@ivan"),
                    });

            using (unitOfWorkProvider.Create())
            {
                acctualResult = await accountQuery.Where(predicate).ExecuteAsync();
            }

            Assert.AreEqual(expecterResult, acctualResult);
        }
    }
}
