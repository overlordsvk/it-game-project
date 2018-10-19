using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public DbSet<Message> Messages { get; set; }
        public DbSet<WeaponType> WeaponTypes { get; set; }

        public GameDbContext() : base()//"Server=tcp:pv179-mol-bal.database.windows.net,1433;Initial Catalog=PV179DB;Persist Security Info=False;User ID=xbaltaz;Password=***REMOVED***;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;")
        {
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
            modelBuilder.Entity<Message>()
                .HasOptional<Character>(m => m.Receiver)
                .WithMany(ch => ch.ReceivedMessages)
                .HasForeignKey<int?>(m => m.ReceiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasOptional<Character>(m => m.Sender)
                .WithMany(ch => ch.SentMessages)
                .HasForeignKey<int?>(m => m.SenderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasOptional<Character>(i => i.Owner)
                .WithMany(ch => ch.Items)
                .HasForeignKey<int?>(i => i.OwnerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasOptional<Character>(i => i.ShopOwner)
                .WithMany(ch => ch.Shop)
                .HasForeignKey<int?>(i => i.ShopOwnerId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
