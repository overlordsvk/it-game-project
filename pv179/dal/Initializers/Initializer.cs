using System;
using System.Data.Entity;
using System.Collections.Generic;
using Game.DAL.Entities;
using Game.DAL.Enums;
using Game.DAL.Entity.Entities;

namespace Game.DAL.Entity.Initializers
{
    public class Initializer : DropCreateDatabaseAlways<GameDbContext>
    {
        private static readonly Guid _guid1 = Guid.Parse("629fd444-0146-4b94-a928-e62a4ab51f42");
        private static readonly Guid _guid2 = Guid.Parse("84124fe7-e59b-411d-a76e-73b81bf233c2");
        private static readonly Guid _guid3 = Guid.Parse("b7701e3c-e06c-4f0d-8aef-4b23901b5d95");
        private static readonly Guid _guid4 = Guid.Parse("bdf0f556-5168-4e1f-8697-94c6f54a09a5");
        private static readonly Guid _guid5 = Guid.Parse("c528f80c-874e-472d-9b54-92606c44be50");
        private static readonly Guid _guid6 = Guid.Parse("4bfc8e4b-85d4-47f5-ae87-080a80f750c5");
        private static readonly Guid _guid7 = Guid.Parse("5092aae4-3f07-4a0e-afbb-bc094a3cc73e");
        private static readonly Guid _guid8 = Guid.Parse("b49c2b2f-99b0-4f21-8655-70b0d2c7c5d2");
        private static readonly Guid _guid9 = Guid.Parse("3454b2db-4bb1-4ffa-a3e9-be8c9617252d");
        private static readonly Guid _guid10 = Guid.Parse( "e9c469c0-60e0-437e-ad8f-c9a742e551b0");
        private static readonly Guid _guid11 = Guid.Parse( "5faff0b1-78ac-4ac1-be7c-dd7a83250a5c");
        private static readonly Guid _guid12 = Guid.Parse( "ad311795-bf7b-4240-a643-4760fee1106c");
        private static readonly Guid _guid13 = Guid.Parse( "325ded93-ea55-42a8-a585-2e5495fe5df1");
        private static readonly Guid _guid14 = Guid.Parse( "12eb575d-be07-43db-be9e-8c7add0e4bb6");
        private static readonly Guid _guid15 = Guid.Parse( "65016352-7a07-442a-b439-5f4516e90c70");
        private static readonly Guid _guid16 = Guid.Parse( "25ce42c3-20e7-4d5c-b9cc-56ab5a64c9f6");
        private static readonly Guid _guid17 = Guid.Parse( "931f192b-452d-469d-aaf4-af9b18886769");
        private static readonly Guid _guid18 = Guid.Parse( "5fd2c0ea-f31d-482b-a1b7-4650495ad346");
        private static readonly Guid _guid19 = Guid.Parse( "abb4f59a-8fed-4060-ac96-be827992054c");
        private static readonly Guid _guid20 = Guid.Parse( "46f7bc5a-c043-4722-9438-6e6e7640fc71");

        protected override void Seed(GameDbContext context)
        {
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

            Character Ch = new Character
            {
                Name = "BelaKing",
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
            Ch.Items = new List<Item>
            {
                itemAxe2
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
                IsAdmin = true,
                Character = Ch,
            };

            

            var accountIvan = new Account
            {
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
            
            context.Characters.Add(characterSlayer);
            context.Characters.Add(characterWalker);
            context.Characters.Add(Ch);

            context.Accounts.Add(accountPeter);
            context.Accounts.Add(accountIvan);
            context.Accounts.Add(accountVedro);

            context.Messages.Add(message1);
            context.Messages.Add(message2);

            context.Fights.Add(fight1);

            //context.Groups.Add(group1);

            //context.GroupPosts.Add(gpost);

            base.Seed(context);
        }
    }
}