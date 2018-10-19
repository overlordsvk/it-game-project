using DynamicRepository.Contract;
using Game.DAL.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entity.Entities
{
    public class Account : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(4), MaxLength(32)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public virtual Character Character { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Accounts);

    }
}
