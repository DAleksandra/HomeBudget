using System;

namespace HomeBudget.API.Models
{
    public class RecurringIncome
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Source { get; set; }
        public DateTime DateAdded { get; set; }
        public float Amount { get; set; }
        public string Interval { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}