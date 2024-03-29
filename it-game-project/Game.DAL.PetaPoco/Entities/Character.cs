﻿using AsyncPoco;
using Game.Infrastructure;
using System;

namespace Game.DAL.PetaPoco.Entities
{
    [TableName(TableNames.CharacterTable)]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Character : IEntity
    {
        public Guid Id { get; set; }

        [Ignore]
        public string TableName { get; } = TableNames.CharacterTable;

        public string Name { get; set; }

        public int Money { get; set; }

        public int Health { get; set; }

        public int Score { get; set; }

        public int Strength { get; set; }

        public int Perception { get; set; }

        public int Endurance { get; set; }

        public int Charisma { get; set; }

        public int Intelligence { get; set; }

        public int Agility { get; set; }

        public int Luck { get; set; }

        public Guid? GroupId { get; set; }

        public Account Account { get; set; }
    }
}