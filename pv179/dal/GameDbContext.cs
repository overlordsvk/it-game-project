using System;
using System.Data.Common;
using System.Data.Entity;
using Game.DAL.Entities;
using Game.DAL.Entity.Config;
using Game.DAL.Entity.Entities;
using Game.DAL.Entity.Initializers;

namespace Game.DAL.Entity
{
    public class GameDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Fight> Fights { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPost> GroupPosts { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Non-parametric ctor used by data access layer
        /// </summary>
        public GameDbContext() : base(EntityInstaller.LocalDbConnection)
        {
            // force load of EntityFramework.SqlServer.dll into build
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer(new Initializer());
        }

        /// <summary>
        /// Ctor with db connection, required by data access layer tests
        /// </summary>
        /// <param name="connection">The database connection</param>
        public GameDbContext(DbConnection connection) : base(connection, true)
        {
            Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>()
                .HasOptional<Character>(ch => ch.Receiver)
                .WithMany(ch => ch.ReceiverChats)
                .HasForeignKey(ch => ch.ReceiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Chat>()
                .HasOptional<Character>(ch => ch.Sender)
                .WithMany(ch => ch.SenderChats)
                .HasForeignKey(ch => ch.SenderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasOptional<Character>(i => i.Owner)
                .WithMany(ch => ch.Items)
                .HasForeignKey<Guid?>(i => i.OwnerId)
                .WillCascadeOnDelete(false);

           modelBuilder.Entity<Message>()
               .HasRequired<Chat>(m => m.Chat)
               .WithMany(ch => ch.Messages)
               .HasForeignKey<Guid?>(m => m.ChatId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fight>()
                .HasOptional<Character>(ch => ch.Attacker)
                .WithMany(ch => ch.AttackerFights)
                .HasForeignKey(ch => ch.AttackerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fight>()
               .HasOptional<Character>(ch => ch.Defender)
               .WithMany(ch => ch.DefenderFights)
               .HasForeignKey(ch => ch.DefenderId)
               .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
