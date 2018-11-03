﻿using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Game.DAL.Entity.Entities;
using System.Threading.Tasks;
using Castle.Windsor;
using dal.Enums;
using NUnit.Framework;
using Game.DAL.Entity;
using Game.DAL.Entity.Config;
using Game.Infrastructure.UnitOfWork;

namespace InfrastructureTests
{
    public class Initializer
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        private const string TestDbConnectionString = "InMemoryTestDBGame";

        public void InitializeTests()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            Database.SetInitializer(new DropCreateDatabaseAlways<GameDbContext>());
            Container.Install(new EntityInstaller());
        }

        private static DbContext InitializeDatabase()
        {
            var context = new GameDbContext(Effort.DbConnectionFactory.CreatePersistent(TestDbConnectionString));
            context.Accounts.RemoveRange(context.Accounts);
            context.Characters.RemoveRange(context.Characters);
            context.Fights.RemoveRange(context.Fights);
            context.GroupPosts.RemoveRange(context.GroupPosts);
            context.Groups.RemoveRange(context.Groups);
            context.Items.RemoveRange(context.Items);
            context.Messages.RemoveRange(context.Messages);
            context.SaveChanges();


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
                itemAxe
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
                itemBow
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
                Name = "Prva skupina"
            };

            group1.Members = new System.Collections.Generic.List<Character>
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



            context.Items.Add(itemAxe);
            context.Items.Add(itemAxe2);
            context.Items.Add(itemBow);

            context.Characters.Add(characterSlayer);
            context.Characters.Add(characterWalker);

            context.Accounts.Add(accountPeter);
            context.Accounts.Add(accountIvan);
            context.Accounts.Add(accountVedro);


            context.Fights.Add(fight1);

            context.Groups.Add(group1);

            context.GroupPosts.Add(gpost);


            context.SaveChanges();

            return context;
        }

    }
}
