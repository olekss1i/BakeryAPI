using System.Collections.Generic;

namespace BakeryAPI.DTOs
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CountryName { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
