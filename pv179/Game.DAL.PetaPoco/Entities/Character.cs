using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncPoco;

namespace Game.DAL.PetaPoco
{
    [TableName(TableNames.CharacterTable)]
    public class Character : IEntity
    {
        public int Id { get; set; }

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

        [Ignore]
        public ICollection<Item> Items { get; set; }
        [Ignore]
        public ICollection<Message> ReceivedMessages { get; set; }
        [Ignore]
        public ICollection<Message> SentMessages { get; set; }
        [Ignore]
        public ICollection<Item> Shop { get; set; }
        public int? GroupId { get; set; }
        [Ignore]
        public Group Group { get; set; }
        [Ignore]
        public Account Account { get; set; }
    }
}
