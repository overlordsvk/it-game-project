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

            CreateTeam(newTeam).Wait();
            Console.WriteLine(GetFirstTeamName().Result);

            Console.ReadKey();
        }

        public static async Task CreateTeam(string newTeam)
        {
            var provider = UnitOfWorkProviderFactory.Create();

            using (var unitOfWork = provider.Create())
            {
                var repo = new Repository<Account>(provider);
                repo.Create(new Account() { Username = newTeam, Email = "jano@jano.com", Password = "123456", IsAdmin = false });
                await unitOfWork.Commit();
            }
        }

        public static async Task<string> GetFirstTeamName()
        {
            var provider = UnitOfWorkProviderFactory.Create();

            using (var unitOfWork = provider.Create())
            {
                var repo = new Repository<Account>(provider);
                var a = await repo.GetAsync(1);
                return a.Username;
            }
        }

    }
}
