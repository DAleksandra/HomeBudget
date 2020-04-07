using System;

namespace HomeBudget.API.DTOs
{
    public class OutgoingForReturnDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Shop { get; set; }
        public DateTime DateAdded { get; set; }
        public float Cost { get; set; }
    }
}