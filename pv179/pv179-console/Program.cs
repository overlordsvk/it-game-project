using DAL.Repository;
using DAL.UnitOfWork;
using DAL.Entities;
using System;
using System.Threading.Tasks;
using DAL;

namespace PV179Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Accounts: ");

            using (var db = new GameDbContext())
            {
                foreach (var acc in db.Accounts)
                {
                    Console.WriteLine(acc.Id + "\t" + acc.Username + "\t" + acc.Email);
                }
            }
            /*
            Console.WriteLine("Add new Account:");
            var accountName = Console.ReadLine();

            CreateAccount(accountName).Wait();
            Console.WriteLine(GetAccountById(1).Result ?? "null");
            */
            Console.ReadKey();
        }

        public static async Task CreateAccount(string accountName)
        {
            var provider = UnitOfWorkProviderFactory.Create();

            using (var unitOfWork = provider.Create())
            {
                var repo = new Repository<Account>(provider);
                repo.Create(new Account() { Username = accountName, Email = accountName+"@gmail.com", Password = "12345d6789", IsAdmin = false });
                await unitOfWork.Commit();
            }
        }

        public static async Task<string> GetAccountById(int id)
        {
            var provider = UnitOfWorkProviderFactory.Create();

            using (var unitOfWork = provider.Create())
            {
                var repo = new Repository<Account>(provider);
                var a = await repo.GetAsync(3);
                return a.Username;
            }
        }

    }
}
