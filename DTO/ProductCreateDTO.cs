using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BakeryAPI.DTOs
{
    public class ProductCreateDTO
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string Recipe { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int OriginCountryId { get; set; }

        public List<int> IngredientIds { get; set; } = new();
    }
}
