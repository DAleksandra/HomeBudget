using System;

namespace HomeBudget.API.Models
{
    public class RecurringOutgoing
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Shop { get; set; }
        public DateTime DateAdded { get; set; }
        public string Interval { get; set; }
        public float Cost { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}