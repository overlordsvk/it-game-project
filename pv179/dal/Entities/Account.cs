using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Game.Infrastructure;

namespace Game.DAL.Entity.Entities
{
    public class Account : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required, MinLength(4), MaxLength(32)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public string Picture { get; set; }

        public virtual Character Character { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Accounts);

    }
}
