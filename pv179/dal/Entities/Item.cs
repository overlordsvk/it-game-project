using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Game.Infrastructure;

namespace Game.DAL.Entity.Entities
{
    public class Item : IEntity
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [Range(0,500)]
        public int Attack { get; set; }

        [Range(0, 500)]
        public int Defense { get; set; }

        [Range(0,100)]
        public int Weight { get; set; }

        [Range(0,int.MaxValue)]
        public int Price { get; set; }

        public bool Equipped { get; set; }

        [ForeignKey(nameof(Owner))]
        public int? OwnerId { get; set; }
        public virtual Character Owner { get; set; }

        [ForeignKey(nameof(ShopOwner))]
        public int? ShopOwnerId { get; set; }
        public virtual Character ShopOwner { get; set; }

        public bool IsWeapon { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Items);
    }
}
