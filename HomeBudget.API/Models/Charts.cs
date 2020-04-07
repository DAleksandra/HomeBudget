using System.Collections.Generic;

namespace HomeBudget.API.Models
{
    public class Charts
    {
        public List<float> Outgoings { get; set; }
        public List<float> Incomes { get; set; }

        public List<string> Dates { get; set; }
        public List<string> DateIncomes { get; set; }
        public IEnumerable<string> DateOutgoings { get; set; }
    }
}