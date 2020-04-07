using System;

namespace HomeBudget.API.DTOs
{
    public class RecurringIncomeForReturnDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Source { get; set; }
        public DateTime DateAdded { get; set; }
        public string Interval { get; set; }
        public float Amount { get; set; }
    }
}