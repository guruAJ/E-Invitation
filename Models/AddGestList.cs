using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Models
{
    public class AddGestList
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public int OcassionId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int EnclosureId { get; set; }
        [Required]
        public int RankId { get; set; }
        [Required]
        public int IndRankId { get; set; }
        [Required]
        public string ArmyNo { get; set; }
        [Required]
        public string IndlName { get; set; }
        [Required]
        public string Unit { get; set; }
        public string Fmn { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string NameOfGest { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Relation { get; set; }
        [Required]
        public string Dob { get; set; }
        public string AdhaorNo { get; set; }
        public string Photo { get; set; }
        public bool IsActive { get; set; }
        public bool IsPass { get; set; }


    }
}
