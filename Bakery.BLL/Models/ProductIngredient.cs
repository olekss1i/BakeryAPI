using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryAPI.Models
{
    public class ProductIngredient
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = "g";

        // Навігаційні властивості
        public virtual Product Product { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}