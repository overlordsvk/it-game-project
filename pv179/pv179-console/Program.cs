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
            WriteTable().Wait();

            using (var db = new GameDbContext())
            {


                Console.WriteLine("\nAccounts: ");
                foreach (var acc in db.Accounts)
                {
                    Console.WriteLine($"{acc.Id} \t  {acc.Username}  \t  {acc.Email}  \t \t Character:   {acc.Character?.Name}");
                }

                Console.WriteLine("\nCharacters: ");
                foreach (var ch in db.Characters)
                {
                    Console.WriteLine($"{ch.Id}  \t  {ch.Name} \t Items: {ch.Items.Count} \t Shop: {ch.Shop.Count} \t {ch.Group.Name} \t  Owner:  {ch.Account.Username}");
                }

                Console.WriteLine("\nWeapon Types: ");
                foreach (var wt in db.WeaponTypes)
                {
                    Console.WriteLine($"{wt.Id} \t {wt.ItemName} \t MA: {wt.MaxAttack:5} MD: {wt.MinDefense:5}");
                }

                Console.WriteLine("\nItems: ");
                foreach (var i in db.Items)
                {
                    Console.WriteLine($"{i.Id} \t {i.Name}  \t  {i.WeaponType.ItemName}  \t \t  Owner: {i.Owner?.Name}");
                }

                Console.WriteLine("\nMessages: ");
                foreach (var m in db.Messages)
                {
                    Console.WriteLine($"{m.Id} \t {m.Sender.Name}  \t  {m.Receiver.Name} \t Sub: {m.Subject} \t  Text: {m.Text} ");
                }

                Console.WriteLine("\nGroups: ");
                foreach (var g in db.Groups)
                {
                    Console.WriteLine($"{g.Id} \t {g.Name} Members: {g.Members.Count} ");
                }

                Console.WriteLine("\nGroupPosts: ");
                foreach (var g in db.GroupPosts)
                {
                    Console.WriteLine($"{g.Id} \t {g.Author.Name} : {g.Text} ");
                }

                Console.WriteLine("\nFights: ");
                foreach (var f in db.Fights)
                {
                    Console.WriteLine($"{f.Id} \t {f.Attacker.Name} \t {f.Defender.Name} \t Ai: {f.AttackerItem.Name} \t Di: {f.DefenderItem.Name} \t Succ: {f.AttackSuccess}");
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
        public static async Task WriteTable()
        {
            var provider = UnitOfWorkProviderFactory.Create();
            using (var unitOfWork = provider.Create())
            {
                var repo = new Repository<Account>(provider);
                var data = await repo.GetAllAsync();
                foreach (var d in data)
                {
                    Console.WriteLine(d.Id + " " + d.Username);
                }
            }
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
