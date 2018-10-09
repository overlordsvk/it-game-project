﻿using DAL.Repository;
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

            PrintDbContent().Wait();

            Console.ReadKey();
            /*using (var dbContext = new GameDbContext())
            {
                dbContext.Database.Delete();
            }*/
        }
        public static async Task PrintDbContent()
        {
            var provider = UnitOfWorkProviderFactory.Create();
            using (var unitOfWork = provider.Create())
            {
                var accountRepo = new Repository<Account>(provider);
                var fightRepo = new Repository<Fight>(provider);
                var groupRepo = new Repository<Group>(provider);
                var characterRepo = new Repository<Character>(provider);
                var grouppostRepo = new Repository<GroupPost>(provider);
                var itemRepo = new Repository<Item>(provider);
                var messageRepo = new Repository<Message>(provider);
                var wtypeRepo = new Repository<WeaponType>(provider);

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
                    Console.WriteLine($"{ch.Id}  \t  {ch.Name} \t Items: {ch.Items.Count} \t Shop: {ch.Shop.Count} \t {ch.Group.Name} \t RM: {ch.ReceivedMessages.Count} \t Owner:  {ch.Account.Username}");
                }

                Console.WriteLine("\nWeapon Types: ");
                foreach (var wt in wtypes)
                {
                    Console.WriteLine($"{wt.Id} \t {wt.ItemName} \t MA: {wt.MaxAttack:5} MD: {wt.MinDefense:5}");
                }

                Console.WriteLine("\nItems: ");
                foreach (var i in items)
                {
                    Console.WriteLine($"{i.Id} \t {i.Name}  \t  {i.WeaponType.ItemName}  \t \t  Owner: {i.Owner?.Name}");
                }

                Console.WriteLine("\nMessages: ");
                foreach (var m in messages)
                {
                    Console.WriteLine($"{m.Id} \t {m.Sender.Name}  \t  {m.Receiver.Name} \t Sub: {m.Subject} \t  Text: {m.Text} ");
                }

                Console.WriteLine("\nGroups: ");
                foreach (var g in groups)
                {
                    Console.WriteLine($"{g.Id} \t {g.Name} Members: {g.Members.Count} ");
                }

                Console.WriteLine("\nGroupPosts: ");
                foreach (var g in groupposts)
                {
                    Console.WriteLine($"{g.Id} \t {g.Author.Name} : {g.Text} ");
                }

                Console.WriteLine("\nFights: ");
                foreach (var f in fights)
                {
                    Console.WriteLine($"{f.Id} \t {f.Attacker.Name} \t {f.Defender.Name} \t Ai: {f.AttackerItem.Name} \t Di: {f.DefenderItem.Name} \t Succ: {f.AttackSuccess}");
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
