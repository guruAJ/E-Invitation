using System.ComponentModel.DataAnnotations;

namespace E_Invitation.Models
{
    public class ECard
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OcassionId { get; set; }
        [Required]
        public string Card1 { get; set; }
        [Required]
        public string Card2 { get; set; }
        [Required]
        public string Card3 { get; set; }

        public int IsActive { get; set; }
    }
}
