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

        [Required, MinLength(4), MaxLength(64)]
        public string Username { get; set; }

        [Required, MinLength(4), MaxLength(320)]
        public string Email { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Roles { get; set; }

        public string Picture { get; set; }

        public virtual Character Character { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(GameDbContext.Accounts);

    }
}
