using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Game.Infrastructure;

namespace Game.DAL.Entity.Entities
{
    public class WeaponType : IEntity
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string ItemName { get; set; }

        [Range(0,500)]
        public int MaxAttack { get; set; }
        [Range(0,500)]
        public int MaxDefense { get; set; }
        [Range(0,100)]
        public int MaxWeight { get; set; }
        [Range(0,500)]
        public int MinAttack { get; set; }
        [Range(0,500)]
        public int MinDefense { get; set; }
        [Range(0,100)]
        public int MinWeight { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.WeaponTypes);
    }
}
