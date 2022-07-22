using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Models
{
    public class Ocassion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Event Name")]
        public string OcassionName { get; set; }
        [Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-dd-MM}", ApplyFormatInEditMode = true)]
        public DateTime OcassionDate { get; set; }




        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Hosted on behalf of Chief Name(ex - Manoj Pande)")]
        [Required]
        public string ChiefName { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Venue { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Time (ex-1030 h (To be Seated by 0950h))")]
        public string Time { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Officer's Dress")]
        public string Dress { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Civilian Dress")]
        public string Dress1 { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Event Coordinator(Branch/Officer)Issuing")]
        public string IssueBranch { get; set; }
       
        [Column(TypeName = "varchar(10)")]
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Contact No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNo { get; set; }
        [Required]
        public string ASCON { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
        [Display(Name = "Pass Status")]
        public Boolean IsLock { get; set; }
        [Display(Name = "Event Status")]
        public Boolean IsFinish { get; set; }
    }
}
