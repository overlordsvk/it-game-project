using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class FightDto : DtoBase
    {
        public Guid? AttackerId { get; set; }

        public Guid? DefenderId { get; set; }

        public Guid? AttackerWeaponId { get; set; }
        public ItemDto AttackerWeapon { get; set; }

        public Guid? AttackerArmorId { get; set; }
        public ItemDto AttackerArmor { get; set; }

        public Guid? DefenderWeaponId { get; set; }
        public ItemDto DefenderWeapon { get; set; }

        public Guid? DefenderArmorId { get; set; }
        public ItemDto DefenderArmor { get; set; }

        public bool AttackSuccess { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
