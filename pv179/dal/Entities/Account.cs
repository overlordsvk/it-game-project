using dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Account : IEntity
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(32)]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public Character Character { get; set; }
    }
}
