using Game.DAL.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BL.Config;
using BL.DTO;
using BL.DTO.Filters;
using BL.Facades;
using BL.QueryObject;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Game.DAL.Entities;
using Game.DAL.Enums;
using Game.Infrastructure;
using Game.Infrastructure.Entity.UnitOfWork;
using Game.Infrastructure.Entity.Repository;
using Game.Infrastructure.Query;
using Game.Infrastructure.UnitOfWork;

namespace PV179Console
{
    class Program
    {
        static void Main(string[] args)
        {

            PrintDbContent().Wait();
            Console.WriteLine("Test");

            Console.ReadKey();
            /*using (var dbContext = new GameDbContext())
            {
                dbContext.Database.Delete();
            }*/

            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)).DefaultContext.Mapper;
            var dtoChar = new CharacterDto
            {
                Name = "Harakter",
            };
            var dto = new AccountDto
            {
                Email = "ja@skuska.cz",
                Character = dtoChar,
                //Id = 500,
                IsAdmin = false,
                Password = "123456789",
                Username = "Jano",
            };
            var dto2 = new AccountDto();
            
            var entity = new Account();
            var itemAxe = new Item
            {
                Name = "Sekera",
                Attack = 20,
                Defense = 5,
                Weight = 12,
                ItemType = ItemType.Weapon
            };

            var itemAxe2 = new Item
            {
                Name = "Lepsia Sekera",
                Attack = 25,
                Defense = 10,
                Weight = 8,
                ItemType = ItemType.Weapon
            };

            var itemBow = new Item
            {
                Name = "Luk",
                Attack = 44,
                Defense = 3,
                Weight = 3,
                ItemType = ItemType.Weapon
            };

            Character characterSlayer = new Character
            {
                Name = "KingSlayer",
                Money = 666,
                Health = 98,
                Score = 12,
                Strength = 5,
                Perception = 8,
                Endurance = 2,
                Charisma = 8,
                Intelligence = 1,
                Agility = 5,
                Luck = 9
            };
            characterSlayer.Items = new List<Item>
            {
                itemAxe,
                itemAxe2,
                itemBow
            };
            
            var chardto = new CharacterDto();
            mapper.Map(dto, entity);
            Console.WriteLine(entity.Username + " =====> " + entity.Character.Name);
            mapper.Map(entity, dto2);
            Console.WriteLine(dto2.Username + " =====> " + dto2.Character.Name);

            mapper.Map(characterSlayer, chardto);

            Console.WriteLine($"{chardto.Name}  itemCount: {chardto.Items.Count}");

            var acccrdto = new AccountCreateDto
            {
                Email = "bela@bugar.com",
                Password = "147852369",
                Username = "Bela"
            };

            using (var container = new WindsorContainer())
            {
                container.Install(FromAssembly.This());
                var accFacade = container.Resolve<AccountFacade>();
                var res = accFacade.GetCustomerAccordingToEmailAsync("navi@ivan.com");
                var resew = accFacade.GetCustomerAccordingToEmailAsync("naviasfa@ivan.com").Result == null;
                Console.WriteLine(resew);
                Console.WriteLine("AccFacUser: " + res.Result.Username);
                bool succ = true;
                var res2 = accFacade.RegisterAccount(acccrdto).Result;
                Console.WriteLine("Succ: " + succ);
                var res3 = accFacade.GetCustomerAccordingToEmailAsync("bela@bugar.com").Result;
                //Console.WriteLine("====>>>>" + res3.Username);
            }

            Console.ReadKey();
            PrintDbContent().Wait();
            Console.ReadKey();

        }
        public static async Task PrintDbContent()
        {
            using (var container = new WindsorContainer())
            {
                container.Install(FromAssembly.This());

                var provider = container.Resolve<IUnitOfWorkProvider>();

                //var provider = EntityUnitOfWorkProvider.Create();
                using (var unitOfWork = provider.Create())
                {
                    var queryObjAccount = new AccountQueryObject(container.Resolve<IMapper>(), container.Resolve<IQuery<Account>>());
                    var res = queryObjAccount.ExecuteQuery(new AccountFilterDto {Email = "navi@ivan.com"}).Result;
                    
                    Console.WriteLine("#####"+res.Items.First().Username);
                    var accountRepo = container.Resolve<IRepository<Account>>(provider);
                    var fightRepo = container.Resolve<IRepository<Fight>>(provider);
                    var groupRepo = container.Resolve<IRepository<Group>>(provider);
                    var characterRepo = container.Resolve<IRepository<Character>>(provider);
                    var testgroupRepo = container.Resolve<IRepository<GroupPost>>(provider);
                    var itemRepo = container.Resolve<IRepository<Item>>(provider);
                    var chatRepo = container.Resolve<IRepository<Chat>>(provider);
                    var messageRepo = container.Resolve<IRepository<Message>>(provider);

                    var accounts = await accountRepo.GetAllAsync();
                    var fights = await fightRepo.GetAllAsync();
                    var groups = await groupRepo.GetAllAsync();
                    var characters = await characterRepo.GetAllAsync();
                    var testgroup = await testgroupRepo.GetAllAsync();
                    var items = await itemRepo.GetAllAsync();
                    var chats = await chatRepo.GetAllAsync();
                    var messages = await messageRepo.GetAllAsync();

                    Console.WriteLine("\nAccounts: ");
                    foreach (var acc in accounts)
                    {
                        Console.WriteLine($"{acc.Id} \t  {acc.Username}  \t  {acc.Email}  \t \t Character:   {acc.Character?.Name}");
                    }

                    

                    Console.WriteLine("\nCharacters: ");
                    foreach (var ch in characters)
                    {
                        Console.WriteLine($"{ch.Id}  \t  {ch.Name} \t Items: {ch.Items.Count}  \t {ch.Group.Name} \t RC: {ch.ReceiverChats.Count} \t SC: {ch.SenderChats.Count} \t Owner:  {ch.Account.Username}");
                    }
                    Console.WriteLine(("\nChats"));
                    foreach (var c in chats)
                    {
                        Console.WriteLine($"{c.Subject} \t Count: {c.Messages.Count}");
                    }

                    Console.WriteLine("\nMessages");
                    foreach (var m in messages)
                    {
                        Console.WriteLine($"{m.Chat.Subject} \t Author: {m.Author.Name} \t Text: {m.Text}");
                    }

                    Console.WriteLine("\nItems: ");
                    foreach (var i in items)
                    {
                        //Console.WriteLine($"{i.Id} \t {i.Name}  \t  {i.WeaponType.ItemName}   \t  \t   Owner: {i.Owner?.Name}");
                        Console.WriteLine("{0,-5}{1,-20}{2,-20}{3,-20}", i.Id, i.Name, i.ItemType.ToString(), "Owner: " + i.Owner?.Name);
                    }

                    /*Console.WriteLine("\nMessages: ");
                    foreach (var m in messages)
                    {
                        Console.WriteLine("{0,-5}{1,-20}{2,-20}{3,-20}{4,-20}", m.Id, m.Sender.Name, m.Receiver.Name, "Sub: " + m.Subject, "Text: " + m.Text);
                    }*/

                    Console.WriteLine("\nGroups: ");
                    foreach (var g in groups)
                    {
                        Console.WriteLine($"{g.Id} \t {g.Name}  Members: {g.Members.Count}  Wall: {g.Wall.Count} ");
                    }

                    Console.WriteLine("\nGroupPosts: ");
                    foreach (var g in testgroup)
                    {
                        Console.WriteLine($"{g.Id} \t {g.Group.Name} \t {g.Author.Name} : {g.Text} ");
                    }

                    Console.WriteLine("\nFights: ");
                    foreach (var f in fights)
                    {
                        Console.WriteLine($"{f.Id} \t {f.Attacker.Name} \t {f.Defender.Name} \t Ai: {f.AttackerWeapon.Name} \t Di: {f.DefenderWeapon.Name} \t Succ: {f.AttackSuccess}");
                    }
                }
            }
        
        }
    }
}
