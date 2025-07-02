using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryAPI.Models
{
    public class Distributor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Summary { get; set; }

        [Required]
        [StringLength(200)]
        public string AddressText { get; set; }

        // Зовнішній ключ
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        // Навігаційні властивості
        public virtual Country Country { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public Distributor()
        {
            Ingredients = new HashSet<Ingredient>();
        }
    }
}