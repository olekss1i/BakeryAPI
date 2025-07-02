using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryAPI.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string Recipe { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [ForeignKey("OriginCountry")]
        public int OriginCountryId { get; set; }
        public virtual Country OriginCountry { get; set; }

        public int? DistributorId { get; set; }
        public virtual Distributor Distributor { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; } = new HashSet<ProductIngredient>();
    }
}