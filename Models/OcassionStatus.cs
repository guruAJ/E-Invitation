using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Models
{
    public class OcassionStatus
    {
        [Key]
        public int Id { get; set; }
        public int OcassionId { get; set; }
        public int UserId { get; set; }
        public int IsStatus { get; set; }
    }
}
