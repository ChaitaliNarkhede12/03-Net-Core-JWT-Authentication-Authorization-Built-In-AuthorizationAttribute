using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
