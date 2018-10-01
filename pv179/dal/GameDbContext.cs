using DAL.Entities;
using DAL.Initializers;
using System;
using System.Collections.Generic;
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

        public GameDbContext() : base("BrowserGame")
        {
            Database.SetInitializer(new Initializer());
        }
    }
}
