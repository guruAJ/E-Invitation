using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Models
{
    public class Vacancy : BaseClass
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Ocassion")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The Event field is required.")]
        public int OcassionId { get; set; }
        [Display(Name = "Enclosure")]
        [Required]
        public int EnclosureId { get; set; }
        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }
        [Display(Name = "Rank")]
        [Required]
        public int RankId { get; set; }
        [Required]
        public int Total { get; set; }
        
        public string RankDesc { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }

    }
    public class BaseClass
    {
        [Display(Name = "Event Name")]
        public string OcassionName { get; set; }
        public string EnclosureName { get; set; }
        public string EnclosureColor { get; set; }
        public string CategoryName { get; set; }
        public string RankName { get; set; }

        public Boolean IsLock { get; set; }
    }
}
