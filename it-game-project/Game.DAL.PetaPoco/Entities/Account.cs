using AsyncPoco;
using Game.Infrastructure;
using System;

namespace Game.DAL.PetaPoco.Entities
{
    [TableName(TableNames.AccountTable)]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Account : IEntity
    {
        public Guid Id { get; set; }

        [Ignore]
        public string TableName { get; } = TableNames.AccountTable;

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; }

        public string Picture { get; set; }

        [Ignore]
        public Character Character { get; set; }
    }
}