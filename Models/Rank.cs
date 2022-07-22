using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Models
{
    public class Rank
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
