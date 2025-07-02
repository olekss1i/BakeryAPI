using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryAPI.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression("M|F", ErrorMessage = "Gender must be 'M' or 'F'")]
        public char Gender { get; set; } // 'M' або 'F'

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        // Зовнішній ключ
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        // Навігаційні властивості
        public virtual Country Country { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public Customer()
        {
            Feedbacks = new HashSet<Feedback>();
        }
    }
}