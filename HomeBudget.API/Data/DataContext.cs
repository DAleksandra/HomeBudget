using HomeBudget.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBudget.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Outgoing> Outgoings { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<RecurringIncome> RecurringIncomes { get; set; }
        public DbSet<RecurringOutgoing> RecurringOutgoings { get; set; }

    }
}