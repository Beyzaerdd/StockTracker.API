﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Data.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IQueryable<T>> GetAllQueryableAsync();

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? take = null,
             bool asNoTracking = false,
            params Func<IQueryable<T>, IQueryable<T>>[] includes
            );

        Task<T> GetByIdAsync(int id);
        Task<T> GetAsync(
           Expression<Func<T, bool>> predicate,
           params Func<IQueryable<T>, IQueryable<T>>[] includes
           );
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    }
}