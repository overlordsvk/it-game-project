using DAL.Entities;
using DAL.Initializers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
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
    }
}
