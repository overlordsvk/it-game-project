using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Game.Infrastructure.UnitOfWork;
using Game.Infrastructure.Entity.UnitOfWork;
using Game.Infrastructure;
using Game.Infrastructure.Query;
using Game.DAL.Entity.Config;
using System.Data.Entity;
using Game.DAL.Entity;
using Game.Infrastructure.Entity.Repository;
using Game.Infrastructure.Entity;
using Game.DAL.Entity.Entities;
using Game.DAL.Enums;
using Game.DAL.Entities;

namespace DAL.EntityFrameWork.Tests.Config
{
    public class EntityFrameworkInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "InMemoryEntityFrameworkTestDb";


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            new EntityInstaller().Install(container, store);
        }

        private static DbContext InitializeDatabase()
        {
            var context = new GameDbContext(Effort.DbConnectionFactory.CreatePersistent(TestDbConnectionString));
            context.Accounts.RemoveRange(context.Accounts);
            context.Chat.RemoveRange(context.Chat);
            context.Characters.RemoveRange(context.Characters);
            context.Fights.RemoveRange(context.Fights);
            context.GroupPosts.RemoveRange(context.GroupPosts);
            context.Groups.RemoveRange(context.Groups);
            context.Messages.RemoveRange(context.Messages);
            context.Items.RemoveRange(context.Items);
            context.SaveChanges();


            var itemAxe = new Item
            {
                Name = "Sekera",
                Attack = 20,
                Defense = 5,
                Weight = 12,
                ItemType = ItemType.Weapon,
                Equipped = true
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
                ItemType = ItemType.Weapon,
                Equipped = true
            };

            var itemArmor = new Item
            {
                Name = "Brnenie",
                Attack = 5,
                Defense = 30,
                Weight = 30,
                ItemType = ItemType.Armor,
                Equipped = true
            };

            var itemArmor2 = new Item
            {
                Name = "Prilba",
                Attack = 1,
                Defense = 15,
                Weight = 8,
                ItemType = ItemType.Armor
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
                itemArmor
            };

            var characterWalker = new Character
            {
                Name = "White Walker",
                Money = 1200,
                Health = 50,
                Score = 89,
                Strength = 9,
                Perception = 2,
                Endurance = 5,
                Charisma = 0,
                Intelligence = 2,
                Agility = 6,
                Luck = 4
            };

            characterWalker.Items = new List<Item>
            {
                itemBow,
                itemArmor2
            };


            Account accountPeter = new Account
            {
                Username = "Pieter",
                Email = "pieter@gmail.com",
                Password = "12345678",
                IsAdmin = true
            };



            var accountIvan = new Account
            {
                Id = 200,
                Username = "Ivan",
                Email = "navi@ivan.com",
                Password = "IvanJeBoh",
                IsAdmin = false,
                Character = characterSlayer
            };

            var accountVedro = new Account
            {
                Username = "Vedro",
                Email = "vedro@vemail.com",
                Password = "QWE123975",
                IsAdmin = false,
                Character = characterWalker
            };

            var group1 = new Group
            {
                Name = "Prva skupina",
                Description = "Toto je prva skupina"
            };

            group1.Members = new List<Character>
            {
                characterSlayer,
                characterWalker
            };


            var fight1 = new Fight
            {
                Attacker = characterSlayer,
                Defender = characterWalker,
                AttackerWeapon = itemAxe,
                DefenderWeapon = itemBow,
                Timestamp = DateTime.Now,
                AttackSuccess = true
            };

            var gpost = new GroupPost
            {
                Author = characterSlayer,
                Group = group1,
                Text = "Hi",
                Timestamp = DateTime.Now
            };

            var chat = new Chat
            {
                Receiver = characterSlayer,
                Sender = characterWalker,
                Subject = "Destruction",
            };

            var message1 = new Message
            {
                Author = characterSlayer,
                Chat = chat,
                Text = "This is war",
                Timestamp = DateTime.Now,

            };

            var message2 = new Message
            {
                Author = characterWalker,
                Chat = chat,
                Text = "ok",
                Timestamp = DateTime.Now,

            };

            context.Items.Add(itemAxe);
            context.Items.Add(itemAxe2);
            context.Items.Add(itemBow);
            context.Items.Add(itemArmor);
            context.Items.Add(itemArmor2);

            context.Characters.Add(characterSlayer);
            context.Characters.Add(characterWalker);

            context.Accounts.Add(accountPeter);
            context.Accounts.Add(accountIvan);
            context.Accounts.Add(accountVedro);

            context.Messages.Add(message1);
            context.Messages.Add(message2);

            context.Chat.Add(chat);

            context.Fights.Add(fight1);

            context.Groups.Add(group1);

            context.GroupPosts.Add(gpost);

            context.SaveChanges();
            return context;
        }
    }
}
