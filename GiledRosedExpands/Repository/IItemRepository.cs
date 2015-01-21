using System.Collections.Generic;
using GiledRosedExpands.Models;

namespace GiledRosedExpands.Repository
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();
        Item Get(int id);
    }
}