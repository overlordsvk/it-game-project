using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class WeaponType
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int MaxAttack { get; set; }
        public int MaxDeffense { get; set; }
        public int MaxWeight { get; set; }
        public int MinAttack { get; set; }
        public int MinDeffense { get; set; }
        public int MinWeight { get; set; }
    }
}
