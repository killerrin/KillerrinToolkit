using KillerrinToolkit.EFCore.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KillerrinToolkit.Authentication.Models.Contracts
{
    public interface IUser : IDBModelBase<int>
    {
        [Required]
        string Username { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonIgnore]
        string PasswordHash { get; set; }

        [DataType(DataType.DateTime)]
        DateTime? PasswordExpiry { get; set; }
    }
}
