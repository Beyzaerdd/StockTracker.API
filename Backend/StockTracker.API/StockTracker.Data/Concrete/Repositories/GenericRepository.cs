using Microsoft.EntityFrameworkCore;
using StockTracker.Data.Abstract;
using StockTracker.Data.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Data.Concrete.Repositories
{
   
        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            private readonly StockTrackerDbContext _context;
            private readonly DbSet<T> _dbSet;

            public GenericRepository(StockTrackerDbContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            public async Task<T> AddAsync(T entity)
            {
                await _dbSet.AddAsync(entity);
                return entity;
            }

            public async Task<int> CountAsync()
            {
                return await _dbSet.CountAsync();
            }


      
            public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
            {
                return await _dbSet.CountAsync(predicate);
           
            }

            public void Delete(T entity)
            {
                _dbSet.Remove(entity);
           
            }

            public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
            {
                return await _dbSet.AnyAsync(predicate);
            }


         
            public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
            {
                return await _dbSet.FirstOrDefaultAsync(predicate);
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _dbSet.ToListAsync();
            }

        public async Task<IQueryable<T>> GetAllQueryableAsync()
        {
            return _dbSet.AsQueryable();
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException(nameof(entities));
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? take = null, bool asNoTracking = true, params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            //eager loading
            foreach (var include in includes)
            {
                query = include(query);
            }


            if (predicate != null)
            {
                query = query.Where(predicate);
            }


            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }


            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => include(current));
            }
            return query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id)
            {
                return await _dbSet.FindAsync(id);
            }

            public void Update(T entity)
            {
                _dbSet.Update(entity);
                _dbSet.Entry(entity).State = EntityState.Modified;
            }
        }
    }

