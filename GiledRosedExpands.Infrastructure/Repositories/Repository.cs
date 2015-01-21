using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiledRosedExpands.Domain.Repositories;

namespace GiledRosedExpands.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> 
        where T : class 
    {
        readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public Task<int> AddAsync(T t)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveAsync(T t)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
