using System;
using System.Collections.Generic;
using GiledRosedExpands.Domain.Models;
using GiledRosedExpands.Domain.Repositories;

namespace GiledRosedExpands.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        List<Item> _itemList;

        public ItemRepository()
        {
            _itemList = new List<Item>()
            {
                new Item{ Id=1, Name = "Item 1", Description = "Description item 1", Price = 10},
                new Item{ Id=2, Name = "Item 2", Description = "Description item 2", Price = 20},
                new Item{ Id=3, Name = "Item 3", Description = "Description item 3", Price = 30},
            };    
        }
        public IEnumerable<Item> GetAll()
        {
            return _itemList;
        }

        public Item Get(int id)
        {
            return _itemList.Find(x => x.Id == id);
        }
    }
}
