using System;
using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.DAL.PetaPoco;
using AsyncPoco;
using System.Collections.Generic;

namespace InfrastructureTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PocoTest()
        {
            var connection = new SQLiteConnection("Data Source=:memory:");
            connection.Open();
            var database = new Database(connection);
            var db = new PetapocoDb();
            var dbc = db.GetConnection();
            //db.GetConnection().OpenSharedConnectionAsync().Wait();
            //dbc.ExecuteAsync("DROP TABLE Accounts;").Wait();
            dbc.ExecuteAsync("CREATE TABLE  Accounts(Id INT PRIMARY KEY, Username TEXT, Email TEXT, Password TEXT, IsAdmin BIT, CharacterId INT);").Wait();
            database.ExecuteAsync("CREATE TABLE  Accounts(Id INT PRIMARY KEY, Username TEXT, Email TEXT, Password TEXT, IsAdmin BIT, CharacterId INT);").Wait();


            Console.WriteLine("HI") ;
            Account accountPeter = new Account
            {
                //Id = 5,
                Username = "Pieter",
                Email = "pieter@gmail.com",
                Password = "12345678",
                IsAdmin = true,
                //CharacterId = 3,
            };
            //database.SaveAsync("Accounts", "Id", accountPeter).Wait();

            dbc.InsertAsync("Accounts", "Id", true, accountPeter).Wait();
            //db.GetConnection().u
            //var acc = database.f SingleAsync<Account>(1).Result;
            //Console.WriteLine(acc.Username);
            //Account a = db.GetConnection().SingleAsync<Account>(1).Result;
            
            foreach (var a in db.GetConnection().FetchAsync<Account>("SELECT * FROM Accounts").Result)
            {
                Console.WriteLine($"{a.Id} - {a.Username}");
            }
        }
    }
}
