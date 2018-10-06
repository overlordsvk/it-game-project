using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Initializers
{
    public class Initializer : DropCreateDatabaseAlways<GameDbContext>
    {
        protected override void Seed(GameDbContext context)
        {
            /*context.Accounts.Add(new Account
            {
                Username = "Pieter",
                Email = "pieter@gmail.com",
                Password = "12345678",
                IsAdmin = true
            });*/

            Character ivan = new Character
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

            context.Accounts.Add(new Account
            {
                Username = "Ivan",
                Email = "navi@ivan.com",
                Password = "IvanJeBoh",
                IsAdmin = false,
                Character = ivan
            });

            context.Accounts.Add(new Account
            {
                Username = "Vedro",
                Email = "vedro@vemail.com",
                Password = "QWE123975",
                IsAdmin = false
            });
            

            context.Characters.Add(ivan);

            base.Seed(context);
        }
    }
}
