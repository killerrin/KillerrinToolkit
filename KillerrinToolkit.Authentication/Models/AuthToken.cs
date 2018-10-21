using KillerrinToolkit.Authentication.Models.Contracts;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KillerrinToolkit.Authentication.Models
{
    public class AuthToken : IAuthToken
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
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string Token { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? TokenExpiry { get; set; }
    }
}
