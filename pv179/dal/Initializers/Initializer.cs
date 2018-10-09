using DAL.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace DAL.Initializers
{
    public class Initializer : CreateDatabaseIfNotExists<GameDbContext>
    {
        protected override void Seed(GameDbContext context)
        {
            var wtypeAxe = new WeaponType
            {
                ItemName = "Axe",
                MaxAttack = 100,
                MaxDefense = 20,
                MaxWeight = 15,
                MinAttack = 10,
                MinDefense = 2,
                MinWeight = 8
            };

            var wtypeBow = new WeaponType
            {
                ItemName = "Bow",
                MaxAttack = 50,
                MaxDefense = 50,
                MaxWeight = 9,
                MinAttack = 3,
                MinDefense = 0,
                MinWeight = 1
            };

            var itemAxe = new Item
            {
                Name = "Sekera",
                Attack = 20,
                Defense = 5,
                Weight = 12,
                WeaponType = wtypeAxe
            };

            var itemAxe2 = new Item
            {
                Name = "Lepsia Sekera",
                Attack = 25,
                Defense = 10,
                Weight = 8,
                WeaponType = wtypeAxe
            };

            var itemBow = new Item
            {
                Name = "Luk",
                Attack = 44,
                Defense = 3,
                Weight = 3,
                WeaponType = wtypeBow
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

            characterWalker.Shop = new List<Item>
            {
                itemAxe2
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
                AttackerItem = itemAxe,
                DefenderItem = itemBow,
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

            var message1 = new Message
            {
                Sender = characterSlayer,
                Receiver = characterWalker,
                Timestamp = DateTime.Now,
                Subject = "Destruction",
                Text = "I will destroy you"
            };

            
            context.WeaponTypes.Add(wtypeAxe);
            context.WeaponTypes.Add(wtypeBow);

            context.Items.Add(itemAxe);
            context.Items.Add(itemAxe2);
            context.Items.Add(itemBow);
            
            context.Characters.Add(characterSlayer);
            context.Characters.Add(characterWalker);
            
            context.Accounts.Add(accountPeter);
            context.Accounts.Add(accountIvan);
            context.Accounts.Add(accountVedro);

            context.Messages.Add(message1);
            
            context.Fights.Add(fight1);
            
            context.Groups.Add(group1);

            context.GroupPosts.Add(gpost);

            base.Seed(context);
        }
    }
}