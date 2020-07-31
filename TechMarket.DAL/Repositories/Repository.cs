using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechMarket.DAL.Interfaces;

namespace TechMarket.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;
        public Repository(DbContext context)
        {
            this.context = context;
        }
        public async ValueTask<T> GetByIdAsync(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }
        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().SingleOrDefaultAsync(predicate);
        }
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);
        }
        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }
        public void Update(T entity)
        {
            //context.Set<T>().Update(entity);
            //context.Entry(entity).State = EntityState.Detached;
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
