using System;
using AsyncPoco;

namespace Game.DAL.PetaPoco
{
    [TableName(TableNames.AccountTable)]
    [PrimaryKey("Id")]

    public class Account : IEntity
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public int? CharacterId { get; set; }
        [Ignore]
        public Character Character { get; set; }

    }
}
