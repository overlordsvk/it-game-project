﻿using Game.DAL.Enums;
using Game.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game.DAL.Entity.Entities
{
    public class Item : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [Range(1, 500)]
        public int Attack { get; set; }

        [Range(1, 500)]
        public int Defense { get; set; }

        [Range(1, 100)]
        public int Weight { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        public bool Equipped { get; set; }

        [ForeignKey(nameof(Owner))]
        public Guid? OwnerId { get; set; }

        public virtual Character Owner { get; set; }

        public ItemType ItemType { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Items);
    }
}