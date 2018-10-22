using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace KillerrinToolkit.Authentication.Models.Safe
{
    public class UserAuthenticated
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string AuthToken { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AuthTokenExpiry { get; set; }
    }
}
