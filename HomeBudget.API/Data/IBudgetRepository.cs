using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeBudget.API.Helpers;
using HomeBudget.API.Models;

namespace HomeBudget.API.Data
{
    public interface IBudgetRepository
    {
         Task<ICollection<Outgoing>> GetOutgoings(int UserId, DataFilter dateFilter);
         Task<ICollection<Outgoing>> GetOutgoings(int UserId);
         Task<List<Income>> GetIncomes(int UserId);
         Task<Outgoing> GetOutgoing(int id);
         Task<ICollection<Income>> GetIncomes(int UserId, DataFilter dateFilter);
         Task<ICollection<Photo>> GetPhotos(int UserId, DataFilter dateFilter);
         Task<Income> GetIncome(int id);
         Task<RecurringIncome> GetRecurringIncome(int id);
         Task<List<RecurringIncome>> GetRecurringIncomes(int UserId);
         Task<RecurringOutgoing> GetRecurringOutgoing(int id);
         Task<List<RecurringOutgoing>> GetRecurringOutgoings(int UserId);
         Task<User> GetUser(int userId);
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<Photo> GetPhoto(int id);
         Task<bool> SaveAll();


    }
}