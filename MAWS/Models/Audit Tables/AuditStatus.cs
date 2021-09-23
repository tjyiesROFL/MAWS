using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAWS.Models
{
    public class AuditTableStatus
    {
        public AuditTableStatus()
        {

        }
        [Key]
        public bool IsActive { get; set; }

    }
}