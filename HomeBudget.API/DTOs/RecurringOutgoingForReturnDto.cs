using System;

namespace HomeBudget.API.DTOs
{
    public class RecurringOutgoingForReturnDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Shop { get; set; }
        public DateTime DateAdded { get; set; }
        public string Interval { get; set; }
        public float Cost { get; set; }
    }
}