﻿using BCrypt.Net;
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
        public int ID { get; set; }

        public string Username { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime? Expiry { get; set; }

        [DataType(DataType.Password)]
        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public string AuthToken { get; set; }
    }
}