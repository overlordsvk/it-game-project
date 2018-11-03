﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncPoco;
using Game.Infrastructure;

namespace Game.DAL.PetaPoco.Entities
{
    [TableName(TableNames.AccountTable)]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Account : IEntity
    {
        public int Id { get; set; }

        [Ignore]
        public string TableName { get; } = TableNames.AccountTable;

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public string Picture { get; set; }

        [Ignore]
        public Character Character { get; set; }
    }
}