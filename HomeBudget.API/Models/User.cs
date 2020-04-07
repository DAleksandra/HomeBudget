using System.Collections.Generic;

namespace HomeBudget.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<Outgoing> Outgoings { get; set; }
        public ICollection<Income> Incomes { get; set; }
        public ICollection<RecurringOutgoing> RecurringOutgoing { get; set; }
        public ICollection<RecurringIncome> RecurringIncome { get; set; }
        public List<Photo> Photos { get; set; }
        
    }
}