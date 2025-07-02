using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryAPI.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Навігаційні властивості
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Distributor> Distributors { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public Country()
        {
            Products = new HashSet<Product>();
            Customers = new HashSet<Customer>();
            Distributors = new HashSet<Distributor>();
            Ingredients = new HashSet<Ingredient>();
        }
    }
}