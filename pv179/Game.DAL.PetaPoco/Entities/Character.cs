using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncPoco;
using Game.Infrastructure;

namespace Game.DAL.PetaPoco.Entities
{
    [TableName(TableNames.CharacterTable)]
    [PrimaryKey("Id", autoIncrement = false)]
    public class Character : IEntity
    {
        public int Id { get; set; }

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

        //public virtual ICollection<Item> Items { get; set; }

        //public virtual ICollection<Chat> Chats { get; set; }

        public int? GroupId { get; set; }
        //public Group Group { get; set; }

        public Account Account { get; set; }
    }
}
