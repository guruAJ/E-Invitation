using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Models
{
    public class UserType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}
