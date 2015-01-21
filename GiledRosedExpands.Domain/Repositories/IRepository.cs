using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GiledRosedExpands.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<int> AddAsync(T t);
        Task<int> RemoveAsync(T t);
        Task<List<T>> GetAllAsync();
    }
}
