using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.API.Helpers;
using HomeBudget.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBudget.API.Data
{
    public class BudgetRepository : IBudgetRepository
    {
        public DataContext _context { get; }
        public BudgetRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Outgoing> GetOutgoing(int id)
        {
            var outgoing = await _context.Outgoings.FirstOrDefaultAsync(u => u.Id == id);

            return outgoing;
        }

        public async Task<ICollection<Outgoing>> GetOutgoings(int UserId, DataFilter dateFilter)
        {
            DateTime startDate = DateTime.Parse(dateFilter.DateStart);
            DateTime endDate = DateTime.Parse(dateFilter.DateEnd);
            var outgoings = await _context.Outgoings.Where(u => u.UserId == UserId)
            .Where(u => u.DateAdded >= startDate && u.DateAdded <= endDate)
            .OrderByDescending(u => u.DateAdded).ToListAsync();

            return outgoings;
        }

        public async Task<ICollection<Outgoing>> GetOutgoings(int UserId)
        {
            var outgoings = await _context.Outgoings.Where(u => u.UserId == UserId)
            .OrderByDescending(u => u.DateAdded).ToListAsync();

            return outgoings;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _context.Users.Include(o => o.Outgoings).Include(i => i.Incomes).Include(o => o.RecurringOutgoing).Include(i => i.RecurringIncome)
            .FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<Income>> GetIncomes(int UserId, DataFilter dateFilter)
        {
            DateTime startDate = DateTime.Parse(dateFilter.DateStart);
            DateTime endDate = DateTime.Parse(dateFilter.DateEnd);
            var incomes = await _context.Incomes.Where(i => i.UserId == UserId)
            .Where(u => u.DateAdded >= startDate && u.DateAdded <= endDate)
            .ToListAsync();

            return incomes;
        }

        public async Task<List<Income>> GetIncomes(int UserId)
        {
            var incomes = await _context.Incomes.Where(i => i.UserId == UserId)
            .ToListAsync();

            return incomes;
        }

        public async Task<Income> GetIncome(int id)
        {
            var income = await _context.Incomes.FirstOrDefaultAsync(i => i.Id == id);

            return income;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<ICollection<Photo>> GetPhotos(int UserId, DataFilter dateFilter)
        {
            DateTime startDate = DateTime.Parse(dateFilter.DateStart);
            DateTime endDate = DateTime.Parse(dateFilter.DateEnd);
            var photos = await _context.Photos.Where(i => i.UserId == UserId)
            .Where(u => u.DateAdded >= startDate && u.DateAdded <= endDate)
            .ToListAsync();

            return photos;
        }

        public async Task<RecurringIncome> GetRecurringIncome(int id)
        {
            var recurringIncome = await _context.RecurringIncomes.Where(i => i.Id == id).FirstOrDefaultAsync();

            return recurringIncome;
        }

        public async Task<List<RecurringIncome>> GetRecurringIncomes(int UserId)
        {

            var recurringIncomes = await _context.RecurringIncomes.Where(i => i.UserId == UserId).ToListAsync();

            return recurringIncomes;
        }

        public async Task<RecurringOutgoing> GetRecurringOutgoing(int id)
        {
            var recurringOutgoing = await _context.RecurringOutgoings.Where(i => i.Id == id).FirstOrDefaultAsync();

            return recurringOutgoing;
        }

        public async Task<List<RecurringOutgoing>> GetRecurringOutgoings(int UserId)
        {
            var recurringOutgoings = await _context.RecurringOutgoings.Where(i => i.UserId == UserId).ToListAsync();

            return recurringOutgoings;
        }
    }
}