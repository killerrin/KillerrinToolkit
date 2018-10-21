using KillerrinToolkit.EFCore.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KillerrinToolkit.Authentication.Models.Contracts
{
    public interface IAuthToken : IDBModelBase<int>
    {
        [Required]
        [ForeignKey(nameof(User))]
        int UserId { get; set; }

        [Required]
        string Token { get; set; }

        [DataType(DataType.DateTime)]
        DateTime? TokenExpiry { get; set; }
    }
}
