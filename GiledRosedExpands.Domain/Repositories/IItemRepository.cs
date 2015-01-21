using System.Collections.Generic;
using GiledRosedExpands.Domain.Models;

namespace GiledRosedExpands.Domain.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();
        Item Get(string name);
    }
}