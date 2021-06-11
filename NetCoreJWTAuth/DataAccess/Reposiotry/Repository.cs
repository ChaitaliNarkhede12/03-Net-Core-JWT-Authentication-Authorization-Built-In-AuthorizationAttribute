using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Reposiotry
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly IUnitOfWork _unitOfWork;
        protected DbSet<T> dbSet;
        protected DbContext dbContext;
        public Repository(IUnitOfWork unitOfWork)
        {
            //_unitOfWork = unitOfWork;

            _unitOfWork = unitOfWork;

            if (_unitOfWork.Context != null)
            {
                dbContext = _unitOfWork.Context;
                dbSet = dbContext.Set<T>();
            }
        }

        public async Task<IEnumerable<T>> Get()
        {
            //return _unitOfWork.Context.Set<T>().AsEnumerable<T>();
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            //return _unitOfWork.Context.Set<T>().Where(predicate).AsEnumerable<T>();
            return await dbSet.Where(predicate).ToListAsync();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public async Task Add(T entity)
        {
            // _unitOfWork.Context.Set<T>().Add(entity);
            await dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            // _unitOfWork.Context.Set<T>().Update(entity);
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            //T existing = _unitOfWork.Context.Set<T>().Find(entity);
            //if (existing != null) _unitOfWork.Context.Set<T>().Remove(existing);

            //T existing = dbSet.Find(entity);
            //if (existing != null)
            //{
            //    dbSet.Remove(existing);
            //}

            dbSet.Remove(entity);
        }

        public int SaveChanges()
        {
            return _unitOfWork.Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.Context.SaveChangesAsync();
        }
    }
}
