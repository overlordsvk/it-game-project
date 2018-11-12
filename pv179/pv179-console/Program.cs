using AutoMapper;
using BL.Config;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Game.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using Game.DAL.Enums;
using Game.DAL.Entity.Entities;
using BL.Facades;
using Game.Infrastructure.UnitOfWork;
using BL.QueryObject;
using BL.DTO.Filters;
using Game.Infrastructure.Query;
using Game.Infrastructure;
using Game.DAL.Entities;
using BL.Services.Items;
using Game.Infrastructure.Entity.UnitOfWork;
using Game.DAL.Entity.Initializers;

namespace PV179Console
{
    public static class Program
    {
        private static readonly Guid guid4 = Guid.Parse("2d5c776f-57bf-4467-b354-43b6b096d3fa");
        private static readonly Guid guid5 = Guid.Parse("0aaa0448-3f09-4de7-aa15-257e9c77f451");
        private static readonly Guid guid6 = Guid.Parse("5b4b60c8-bfce-423d-a559-72fd78f4c301");
        private static readonly Guid guid7 = Guid.Parse("e3fdb475-3fcb-4554-a9d5-a22193ebab4c");
        private static readonly Guid guid8 = Guid.Parse("c4d77057-ae1a-493d-ad24-39c6bfd6a507");
        private static readonly Guid guid9 = Guid.Parse("bc53f485-942d-4a41-9e9a-a9ca3b353c56");
        private static readonly Guid guid10 = Guid.Parse("fd4e4768-037f-4ed9-8520-dd1e5a38816c");


        static void Main(string[] args)
        {
            //var context = new GameDbContext(Effort.DbConnectionFactory.CreatePersistent("InMemoryTest"));

            Console.WriteLine("Test");
            PrintDbContent().Wait();

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
                Name = "Sekerisko",
                Attack = 20,
                Defense = 5,
                Weight = 12,
                ItemType = ItemType.Weapon
            };

            var itemAxe2 = new Item
            {
                Name = "Lepsia Sekerka",
                Attack = 25,
                Defense = 10,
                Weight = 8,
                ItemType = ItemType.Weapon
            };

            var itemBow = new Item
            {
                Name = "Lukoslav",
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

            CharacterDto characterBela = new CharacterDto
            {
                Name = "BelaChar",
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
            //characterBela.Items = new List<Item>
            //{
            //    itemAxe,
            //    itemAxe2,
            //    itemBow
            //};

            var chardto = new CharacterDto();
            mapper.Map(dto, entity);
            //Console.WriteLine(entity.Username + " =====> " + entity.Character.Name);
            mapper.Map(entity, dto2);
            //Console.WriteLine(dto2.Username + " =====> " + dto2.Character.Name);

            mapper.Map(characterSlayer, chardto);

            //Console.WriteLine($"{chardto.Name}  itemCount: {chardto.Items.Count}");

            var acccrdto = new AccountCreateDto
            {
                Email = "beliak@bugar.com",
                Password = "147852369",
                Username = "Bela",
            };

            var acccrdto2 = new AccountCreateDto
            {
                Email = "R@Kalinas.com",
                Password = "147852369",
                Username = "Kalinas"
            };

            var acccrdto3 = new AccountCreateDto
            {
                Email = "c@c.com",
                Password = "147852369",
                Username = "cccc"
            };

            using (var container = new WindsorContainer())
            {
                container.Install(FromAssembly.This());
                var accFacade = container.Resolve<AccountFacade>();
                var grFacade = container.Resolve<GroupFacade>();
                var characterFacade = container.Resolve<CharacterFacade>();
                var itemService = container.Resolve<IItemService>();
                var uowp = container.Resolve<IUnitOfWorkProvider>();

                //var res = accFacade.GetAccountAccordingToEmailAsync("navi@ivan.com");
                //var resew = accFacade.GetAccountAccordingToEmailAsync("naviasfa@ivan.com").Result == null;
                //Console.WriteLine(resew);
                //Console.WriteLine("AccFacUser: " + res.Result.Username);
                var res2 = accFacade.RegisterAccount(acccrdto).Result;
                Console.WriteLine("Reggg : " + res2);
                var res22 = accFacade.RegisterAccount(acccrdto2).Result;
                Console.WriteLine("Reg : " + res22);
                var res23 = accFacade.RegisterAccount(acccrdto3).Result;
                Console.WriteLine("Reg : " + res23);
                Console.WriteLine(accFacade.GetAccountAccordingToEmailAsync(acccrdto3.Email).Result.Email);



                var res44 = accFacade.Login("Bela", "147852369").Result;
                Console.WriteLine("Login: " + res44?.Username);
                var res4 = accFacade.RegisterAccount(acccrdto).Result;
                //Console.WriteLine("Succ: ");
                var res3 = accFacade.GetAccountAccordingToUsernameAsync("Bela").Result;
                //Console.WriteLine("====>>>>" + res3.Username);
                var creationId = characterFacade.CreateCharacter(res2, characterBela).Result;
                Console.WriteLine("CreationId====>>>>" + creationId);
                //characterFacade.RemoveCharacter(creationId).Wait();

                var res5 = accFacade.RemoveAccountAsync(Initializer._guid8).Result;
                Console.WriteLine("Remove : " + res5);

                var res6 = grFacade.CreateGroup(creationId, "Most", "Hid", string.Empty).Result;
                Console.WriteLine("GroupCreate: " + res6);

                characterFacade.JoinGroup(accFacade.GetAccountAccordingToUsernameAsync("Vedro").Result.Character.Id, res6).Wait();

                var res7 = characterFacade.Attack(Initializer._guid9, Initializer._guid7).Result;
                Console.WriteLine("Attack: " + res7);

                var gpost = new GroupPostDto
                {
                    CharacterId = creationId,
                    GroupId = res6,
                    Text = "Hi",
                    Timestamp = DateTime.Now,

                };
                grFacade.CreatePost(gpost);

                //var b = characterFacade.GetCharacterById(3).Result;
                //Console.WriteLine("Money:" + b.Money);
                //var res8 = characterFacade.BuyItemAsync(3).Result;
                //b = characterFacade.GetCharacterById(3).Result;
                //Console.WriteLine("Money:" + b.Money);



                //var ch = characterFacade.GetCharacterById(2).Result;
                //Console.WriteLine("Money:" + ch.Money);
                //characterFacade.AddMoneyToCharacter(2, 200).Wait();
                //ch = characterFacade.GetCharacterById(2).Result;
                //Console.WriteLine("Money:" + ch.Money);
                //using(var uow = uowp.Create())
                //{
                //    var i = itemService.GetEquippedWeapon(2).Result;
                //    Console.WriteLine("Equipped Weapon: " + i.Name);

                //}
                //var res9 = characterFacade.EquipItem(3, 3).Result;
                //characterFacade.EquipItem(3, 4).Wait();

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
                    var res = queryObjAccount.ExecuteQuery(new AccountFilterDto { Email = "navi@ivan.com" }).Result;

                    Console.WriteLine("#####" + res.Items.FirstOrDefault()?.Username);
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
                        Console.WriteLine($"{ch.Id}  \t  {ch.Name} \t Items: {ch.Items?.Count}  \t {ch.Group?.Name} \t RC: {ch.ReceiverChats?.Count} \t SC: {ch.SenderChats?.Count} \t Owner:  {ch.Account?.Username}");
                    }
                    Console.WriteLine(("\nChats"));
                    foreach (var c in chats)
                    {
                        Console.WriteLine($"{c.Subject} \t Count: {c.Messages?.Count}");
                    }

                    Console.WriteLine("\nMessages");
                    foreach (var m in messages)
                    {
                        Console.WriteLine($"{m.Chat?.Subject} \t Author: {m.Author?.Name} \t Text: {m.Text}");
                    }

                    Console.WriteLine("\nItems: ");
                    foreach (var i in items)
                    {
                        //Console.WriteLine($"{i.Id} \t {i.Name}  \t  {i.WeaponType.ItemName}   \t  \t   Owner: {i.Owner?.Name}");
                        Console.WriteLine("{0,-5}  {1,-20}{2,-20}{3,-20}{4,-20}", i.Id, i.Name, i.ItemType.ToString(), "Owner: " + i.Owner?.Name, "E: " + i.Equipped);
                    }

                    /*Console.WriteLine("\nMessages: ");
                    foreach (var m in messages)
                    {
                        Console.WriteLine("{0,-5}{1,-20}{2,-20}{3,-20}{4,-20}", m.Id, m.Sender.Name, m.Receiver.Name, "Sub: " + m.Subject, "Text: " + m.Text);
                    }*/

                    Console.WriteLine("\nGroups: ");
                    foreach (var g in groups)
                    {
                        Console.WriteLine($"{g.Id} \t {g.Name}  Members: {g.Members?.Count}  Wall: {g.Wall?.Count} ");
                    }

                    Console.WriteLine("\nGroupPosts: ");
                    foreach (var g in testgroup)
                    {
                        Console.WriteLine($"{g.Id} \t {g.Group?.Name} \t {g.Author?.Name} : {g.Text} ");
                    }

                    Console.WriteLine("\nFights: ");
                    foreach (var f in fights)
                    {
                        Console.WriteLine($"{f.Id} \t {f.Attacker?.Name} \t {f.Defender?.Name} \t Ai: {f.AttackerWeapon?.Name} \t Di: {f.DefenderArmor?.Name} \t Succ: {f.AttackSuccess}");
                    }
                }
            }

        }
    }
}
