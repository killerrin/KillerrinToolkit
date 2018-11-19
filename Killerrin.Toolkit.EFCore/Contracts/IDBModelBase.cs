using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Killerrin.Toolkit.EFCore.Contracts
{
    public interface IDBModelBase<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        T Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        DateTime CreatedOn { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        DateTime ModifiedOn { get; set; }
    }
}
