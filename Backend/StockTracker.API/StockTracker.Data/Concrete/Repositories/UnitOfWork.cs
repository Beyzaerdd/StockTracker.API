using Microsoft.Extensions.DependencyInjection;
using StockTracker.Data.Abstract;
using StockTracker.Data.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Data.Concrete.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StockTrackerDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(StockTrackerDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<IGenericRepository<T>>();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
