using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.PetaPoco
{
    [TableName(TableNames.WeaponTypeTable)]
    public class WeaponType : IEntity
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public int MaxAttack { get; set; }
        public int MaxDefense { get; set; }
        public int MaxWeight { get; set; }
        public int MinAttack { get; set; }
        public int MinDefense { get; set; }
        public int MinWeight { get; set; }
    }
}
