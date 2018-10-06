using DAL.Repository;
using DAL.UnitOfWork;
using DAL;
using DAL.Entities;
using System;
using System.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace PV179Console
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Add new teams:");
            var newTeam = Console.ReadLine();

            CreateAccount(newTeam).Wait();
            Console.WriteLine(GetAccountById(3).Result ?? "null");

            Console.ReadKey();
        }

        public static async Task CreateAccount(string newTeam)
        {
            var provider = UnitOfWorkProviderFactory.Create();

            using (var unitOfWork = provider.Create())
            {
                var repo = new Repository<Account>(provider);
                repo.Create(new Account() { Username = newTeam, Email = "jano@jano.com", Password = "123456789", IsAdmin = false });
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
