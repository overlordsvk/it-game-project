using DAL;
using DAL.Entities;
using System;
using System.Linq;

namespace PV179Console
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Player:");

            using (var db = new GameDbContext())
            {
                var accounts = db.Accounts.ToList();
                //Console.WriteLine(teams.Last().Character.Name);
                
                foreach (var account in accounts)
                {
                    Console.WriteLine(account.Username);
                }
            }
            Console.ReadKey();
        }
    }
}
