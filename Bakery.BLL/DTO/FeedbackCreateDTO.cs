using System.ComponentModel.DataAnnotations;

namespace BakeryAPI.DTOs
{
    public class FeedbackCreateDTO
    {
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}