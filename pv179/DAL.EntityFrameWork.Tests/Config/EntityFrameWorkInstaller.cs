using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Game.DAL.Entities;
using Game.DAL.Entity;
using Game.DAL.Entity.Entities;
using Game.DAL.Enums;
using Game.Infrastructure;
using Game.Infrastructure.Entity;
using Game.Infrastructure.Entity.Repository;
using Game.Infrastructure.Entity.UnitOfWork;
using Game.Infrastructure.Query;
using Game.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Cryptography;

namespace DAL.EntityFrameWork.Tests.Config
{
    public class EntityFrameworkInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "InMemoryEntityFrameworkTestDb";
        public static readonly Guid _guid1 = Guid.Parse("629fd444-0146-4b94-a928-e62a4ab51f42");
        public static readonly Guid _guid2 = Guid.Parse("84124fe7-e59b-411d-a76e-73b81bf233c2");
        public static readonly Guid _guid3 = Guid.Parse("b7701e3c-e06c-4f0d-8aef-4b23901b5d95");
        public static readonly Guid _guid4 = Guid.Parse("bdf0f556-5168-4e1f-8697-94c6f54a09a5");
        public static readonly Guid _guid5 = Guid.Parse("c528f80c-874e-472d-9b54-92606c44be50");
        public static readonly Guid _guid6 = Guid.Parse("4bfc8e4b-85d4-47f5-ae87-080a80f750c5");
        public static readonly Guid _guid7 = Guid.Parse("5092aae4-3f07-4a0e-afbb-bc094a3cc73e");
        public static readonly Guid _guid8 = Guid.Parse("b49c2b2f-99b0-4f21-8655-70b0d2c7c5d2");
        public static readonly Guid _guid9 = Guid.Parse("3454b2db-4bb1-4ffa-a3e9-be8c9617252d");
        public static readonly Guid _guid10 = Guid.Parse("e9c469c0-60e0-437e-ad8f-c9a742e551b0");
        public static readonly Guid _guid11 = Guid.Parse("5faff0b1-78ac-4ac1-be7c-dd7a83250a5c");
        public static readonly Guid _guid12 = Guid.Parse("ad311795-bf7b-4240-a643-4760fee1106c");
        public static readonly Guid _guid13 = Guid.Parse("325ded93-ea55-42a8-a585-2e5495fe5df1");
        public static readonly Guid _guid14 = Guid.Parse("12eb575d-be07-43db-be9e-8c7add0e4bb6");
        public static readonly Guid _guid15 = Guid.Parse("65016352-7a07-442a-b439-5f4516e90c70");
        public static readonly Guid _guid16 = Guid.Parse("25ce42c3-20e7-4d5c-b9cc-56ab5a64c9f6");
        public static readonly Guid _guid17 = Guid.Parse("931f192b-452d-469d-aaf4-af9b18886769");
        public static readonly Guid _guid18 = Guid.Parse("5fd2c0ea-f31d-482b-a1b7-4650495ad346");
        public static readonly Guid _guid19 = Guid.Parse("abb4f59a-8fed-4060-ac96-be827992054c");
        public static readonly Guid _guid20 = Guid.Parse("46f7bc5a-c043-4722-9438-6e6e7640fc71");

        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(InitializeDatabase)
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EntityUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EntityRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EntityQuery<>))
                    .LifestyleTransient()
            );
        }

        private DbContext InitializeDatabase()
        {
            var context = new GameDbContext(Effort.DbConnectionFactory.CreateTransient());
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
                Id = _guid1,
                Name = "Sekera",
                Attack = 20,
                Defense = 5,
                Weight = 12,
                ItemType = ItemType.Weapon,
                Equipped = true
            };

            var itemAxe2 = new Item
            {
                Id = _guid2,
                Name = "Lepsia Sekera",
                Attack = 25,
                Defense = 10,
                Weight = 8,
                ItemType = ItemType.Weapon
            };

            var itemBow = new Item
            {
                Id = _guid3,
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
                Charisma = 1,
                Intelligence = 2,
                Agility = 6,
                Luck = 4
            };

            characterWalker.Items = new List<Item>
            {
                itemBow
            };

            var pass1 = CreateHash("12345678");
            Account accountPeter = new Account
            {
                Id = _guid7,
                Username = "Pieter",
                Email = "pieter@gmail.com",
                PasswordHash = pass1.Item1,
                PasswordSalt = pass1.Item2,
                Roles = "Admin,HasCharacter",
                Character = Ch,
            };

            var pass2 = CreateHash("IvanJeBoh");
            var accountIvan = new Account
            {
                Id = _guid8,
                Username = "Ivan",
                Email = "navi@ivan.com",
                PasswordHash = pass2.Item1,
                PasswordSalt = pass2.Item2,
                Roles = ",HasCharacter",
                Character = characterSlayer
            };

            var pass3 = CreateHash("QWE123975");
            var accountVedro = new Account
            {
                Id = _guid9,
                Username = "Vedro",
                Email = "vedro@vemail.com",
                PasswordHash = pass3.Item1,
                PasswordSalt = pass3.Item2,
                Roles = ",HasCharacter",
                Character = characterWalker
            };

            var group1 = new Group
            {
                Id = _guid10,
                Name = "Prva skupina",
                Description = "Toto je prva a posledna skupina!",
                Picture = "/img/img1.jpg"
            };

            group1.Members = new List<Character>
            {
                characterSlayer,
                characterWalker
            };

            var fight1 = new Fight
            {
                Id = _guid11,
                Attacker = characterSlayer,
                Defender = characterWalker,
                AttackerWeapon = itemAxe,
                DefenderWeapon = itemBow,
                Timestamp = DateTime.Now,
                AttackSuccess = true
            };

            var gpost = new GroupPost
            {
                Id = _guid12,
                Author = characterSlayer,
                Group = group1,
                Text = "Hi",
                Timestamp = DateTime.Now
            };

            var chat = new Chat
            {
                Id = _guid13,
                Receiver = characterSlayer,
                Sender = characterWalker,
                Subject = "Destruction",
            };

            var message1 = new Message
            {
                Id = _guid14,
                Author = characterSlayer,
                Chat = chat,
                Text = "This is war",
                Timestamp = DateTime.Now,
            };

            var message2 = new Message
            {
                Id = _guid15,
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

            context.Groups.Add(group1);

            context.GroupPosts.Add(gpost);

            //var itemAxe = new Item
            //{
            //    Name = "Sekera",
            //    Attack = 20,
            //    Defense = 5,
            //    Weight = 12,
            //    ItemType = ItemType.Weapon,
            //    Equipped = true
            //};

            //var itemAxe2 = new Item
            //{
            //    Name = "Lepsia Sekera",
            //    Attack = 25,
            //    Defense = 10,
            //    Weight = 8,
            //    ItemType = ItemType.Weapon
            //};

            //var itemBow = new Item
            //{
            //    Name = "Luk",
            //    Attack = 44,
            //    Defense = 3,
            //    Weight = 3,
            //    ItemType = ItemType.Weapon,
            //    Equipped = true
            //};

            //var itemArmor = new Item
            //{
            //    Name = "Brnenie",
            //    Attack = 5,
            //    Defense = 30,
            //    Weight = 30,
            //    ItemType = ItemType.Armor,
            //    Equipped = true
            //};

            //var itemArmor2 = new Item
            //{
            //    Name = "Prilba",
            //    Attack = 1,
            //    Defense = 15,
            //    Weight = 8,
            //    ItemType = ItemType.Armor
            //};

            //Character characterSlayer = new Character
            //{
            //    Name = "KingSlayer",
            //    Money = 666,
            //    Health = 98,
            //    Score = 12,
            //    Strength = 5,
            //    Perception = 8,
            //    Endurance = 2,
            //    Charisma = 8,
            //    Intelligence = 1,
            //    Agility = 5,
            //    Luck = 9
            //};

            //characterSlayer.Items = new List<Item>
            //{
            //    itemAxe,
            //    itemAxe2,
            //    itemArmor
            //};

            //var characterWalker = new Character
            //{
            //    Name = "White Walker",
            //    Money = 1200,
            //    Health = 50,
            //    Score = 89,
            //    Strength = 9,
            //    Perception = 2,
            //    Endurance = 5,
            //    Charisma = 1,
            //    Intelligence = 2,
            //    Agility = 6,
            //    Luck = 4
            //};

            //characterWalker.Items = new List<Item>
            //{
            //    itemBow,
            //    itemArmor2
            //};

            //Account accountPeter = new Account
            //{
            //    Username = "Pieter",
            //    Email = "pieter@gmail.com",
            //    PasswordHash = "12345678",
            //    PasswordSalt = "12345678",
            //    Roles = ""
            //};

            //var accountIvan = new Account
            //{
            //    Username = "Ivan",
            //    Email = "navi@ivan.com",
            //    PasswordHash = "IvanJeBoh",
            //    PasswordSalt = "IvanJeBoh",
            //    Roles = "",
            //    Character = characterSlayer
            //};

            //var accountVedro = new Account
            //{
            //    Username = "Vedro",
            //    Email = "vedro@vemail.com",
            //    PasswordHash = "QWE123975",
            //    PasswordSalt = "QWE123975",
            //    Roles = "",
            //    Character = characterWalker
            //};

            //var group1 = new Group
            //{
            //    Name = "Prva skupina",
            //    Description = "Toto je prva skupina"
            //};

            //group1.Members = new List<Character>
            //{
            //    characterSlayer,
            //    characterWalker
            //};

            //var fight1 = new Fight
            //{
            //    Attacker = characterSlayer,
            //    Defender = characterWalker,
            //    AttackerWeapon = itemAxe,
            //    DefenderWeapon = itemBow,
            //    Timestamp = DateTime.Now,
            //    AttackSuccess = true
            //};

            //var gpost = new GroupPost
            //{
            //    Author = characterSlayer,
            //    Group = group1,
            //    Text = "Hi",
            //    Timestamp = DateTime.Now
            //};

            //var chat = new Chat
            //{
            //    Receiver = characterSlayer,
            //    Sender = characterWalker,
            //    Subject = "Destruction",
            //};

            //var message1 = new Message
            //{
            //    Author = characterSlayer,
            //    Chat = chat,
            //    Text = "This is war",
            //    Timestamp = DateTime.Now,

            //};

            //var message2 = new Message
            //{
            //    Author = characterWalker,
            //    Chat = chat,
            //    Text = "ok",
            //    Timestamp = DateTime.Now,

            //};

            //context.Items.Add(itemAxe);
            //context.Items.Add(itemAxe2);
            //context.Items.Add(itemBow);
            //context.Items.Add(itemArmor);
            //context.Items.Add(itemArmor2);

            //context.Characters.Add(characterSlayer);
            //context.Characters.Add(characterWalker);

            //context.Accounts.Add(accountPeter);
            //context.Accounts.Add(accountIvan);
            //context.Accounts.Add(accountVedro);

            //context.Messages.Add(message1);
            //context.Messages.Add(message2);

            //context.Chat.Add(chat);

            //context.Fights.Add(fight1);

            //context.Groups.Add(group1);

            //context.GroupPosts.Add(gpost);

            context.SaveChanges();
            return context;
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }
    }
}