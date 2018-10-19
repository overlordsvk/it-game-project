using Game.DAL.Entity.Entities;
using System;
using System.Threading.Tasks;
using Game.Infrastructure.Entity.UnitOfWork;
using Game.Infrastructure.Entity.Repository;
using Game.Infrastructure.Entity.Counter;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PV179Console
{
    class Program
    {
        static void Main(string[] args)
        {

            PrintDbContent().Wait();

            Console.ReadKey();
            /*using (var dbContext = new GameDbContext())
            {
                dbContext.Database.Delete();
            }*/
        }
        public static async Task PrintDbContent()
        {
            var provider = EntityUnitOfWorkProviderFactory.Create();
            using (var unitOfWork = provider.Create())
            {
                var accountRepo = new EntityRepository<Account>(provider, false, false);
                var accountCounter = new EntityCounter<Account>(false, false);
                var clauses = new List<string> { "Password.Contains(\"123\")" };
                var users = accountRepo.FindAll(clauses);
                Console.WriteLine("\nAccounts: ");
                foreach (var acc in users)
                {
                    Console.WriteLine($"{acc.Id} \t  {acc.Username}  \t  {acc.Email}  \t \t Character:   {acc.Character?.Name}");
                }


                /*
                var accountRepo = new EntityRepository<Account>(provider);
                var fightRepo = new EntityRepository<Fight>(provider);
                var groupRepo = new EntityRepository<Group>(provider);
                var characterRepo = new EntityRepository<Character>(provider);
                var grouppostRepo = new EntityRepository<GroupPost>(provider);
                var itemRepo = new EntityRepository<Item>(provider);
                var messageRepo = new EntityRepository<Message>(provider);
                var wtypeRepo = new EntityRepository<WeaponType>(provider);

                var accounts = await accountRepo.GetAllAsync();
                var fights = await fightRepo.GetAllAsync();
                var groups = await groupRepo.GetAllAsync();
                var characters = await characterRepo.GetAllAsync();
                var groupposts = await grouppostRepo.GetAllAsync();
                var items = await itemRepo.GetAllAsync();
                var messages = await messageRepo.GetAllAsync();
                var wtypes = await wtypeRepo.GetAllAsync();

                Console.WriteLine("\nAccounts: ");
                foreach (var acc in accounts)
                {
                    Console.WriteLine($"{acc.Id} \t  {acc.Username}  \t  {acc.Email}  \t \t Character:   {acc.Character?.Name}");
                }

                Console.WriteLine("\nCharacters: ");
                foreach (var ch in characters)
                {
                    Console.WriteLine($"{ch.Id}  \t  {ch.Name} \t Items: {ch.Items.Count} \t Shop: {ch.Shop.Count} \t {ch.Group.Name} \t RM: {ch.ReceivedMessages.Count} \t SM: {ch.SentMessages.Count} \t Owner:  {ch.Account.Username}");
                }

                Console.WriteLine("\nWeapon Types: ");
                foreach (var wt in wtypes)
                {
                    Console.WriteLine($"{wt.Id} \t {wt.ItemName} \t MA: {wt.MaxAttack:5} MD: {wt.MinDefense:5}");
                }

                Console.WriteLine("\nItems: ");
                foreach (var i in items)
                {
                    Console.WriteLine($"{i.Id} \t {i.Name}  \t  \t  {i.WeaponType.ItemName}   \t  \t   Owner: {i.Owner?.Name}");
                }

                Console.WriteLine("\nMessages: ");
                foreach (var m in messages)
                {
                    Console.WriteLine($"{m.Id} \t {m.Sender.Name}  \t  {m.Receiver.Name} \t Sub: {m.Subject} \t  Text: {m.Text} ");
                }

                Console.WriteLine("\nGroups: ");
                foreach (var g in groups)
                {
                    Console.WriteLine($"{g.Id} \t {g.Name}  Members: {g.Members.Count}  Wall: {g.Wall.Count} ");
                }

                Console.WriteLine("\nGroupPosts: ");
                foreach (var g in groupposts)
                {
                    Console.WriteLine($"{g.Id} \t {g.Group.Name} \t {g.Author.Name} : {g.Text} ");
                }

                Console.WriteLine("\nFights: ");
                foreach (var f in fights)
                {
                    Console.WriteLine($"{f.Id} \t {f.Attacker.Name} \t {f.Defender.Name} \t Ai: {f.AttackerItem.Name} \t Di: {f.DefenderItem.Name} \t Succ: {f.AttackSuccess}");
                }
                */
            }
        }

        public static async Task CreateAccount(string accountName)
        {
            var provider = EntityUnitOfWorkProviderFactory.Create();

            using (var unitOfWork = provider.Create())
            {
                var repo = new EntityRepository<Account>(provider, false, false);
                repo.Create(new Account() { Username = accountName, Email = accountName+"@gmail.com", Password = "12345d6789", IsAdmin = false });
                await unitOfWork.Commit();
            }
        }

        public static async Task<string> GetAccountById(int id)
        {
            var provider = EntityUnitOfWorkProviderFactory.Create();

            using (var unitOfWork = provider.Create())
            {
                var repo = new EntityRepository<Account>(provider, false, false);
                var a = await repo.GetAsync(3);
                return a.Username;
            }
        }

    }
}
