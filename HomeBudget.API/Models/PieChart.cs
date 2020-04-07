using System.Collections.Generic;

namespace HomeBudget.API.Models
{
    public class PieChart
    {
        public IEnumerable<string> IncomeCategories { get; set; }
        public IEnumerable<string> OutgoingCategories { get; set; }
        public List<float> Incomes { get; set; }
        public List<float> Outgoings { get; set; }
    }
}