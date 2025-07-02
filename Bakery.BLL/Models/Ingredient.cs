using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryAPI.Models
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Зовнішні ключі
        [ForeignKey("OriginCountry")]
        public int OriginCountryId { get; set; }

        [ForeignKey("Distributor")]
        public int DistributorId { get; set; }

        // Навігаційні властивості
        public virtual Country OriginCountry { get; set; }
        public virtual Distributor Distributor { get; set; }
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }

        public Ingredient()
        {
            ProductIngredients = new HashSet<ProductIngredient>();
        }
    }
}