using System;
using System.ComponentModel.DataAnnotations;

namespace BakeryAPI.DTOs
{
    public class FeedbackResponseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
