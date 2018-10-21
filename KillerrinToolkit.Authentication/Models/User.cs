using BCrypt.Net;
using KillerrinToolkit.Authentication.Models.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KillerrinToolkit.Authentication.Models
{
    public class User : IUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ModifiedOn { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonIgnore]
        public string PasswordHash { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? PasswordExpiry { get; set; }

        public ICollection<AuthToken> AuthTokens { get; set; } = new List<AuthToken>();

    }
}
