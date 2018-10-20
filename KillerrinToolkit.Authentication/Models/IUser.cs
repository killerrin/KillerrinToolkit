using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KillerrinToolkit.Authentication.Models
{
    public interface IUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int ID { get; set; }

        string Username { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        string Email { get; set; }

        DateTime? Expiry { get; set; }

        [DataType(DataType.Password)]
        [JsonIgnore]
        string PasswordHash { get; set; }

        [JsonIgnore]
        string AuthToken { get; set; }
    }
}
