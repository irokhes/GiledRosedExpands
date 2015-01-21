using System;
using System.Collections.Generic;
using System.Linq;
using GiledRosedExpands.Domain.Models;
using GiledRosedExpands.Domain.Repositories;

namespace GiledRosedExpands.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        List<Purchase> _purchaseList;

        public PurchaseRepository()
        {
            _purchaseList = new List<Purchase>();
        }
        public Purchase Get(int id)
        {
            return _purchaseList.Find(x => x.Id == id);
        }

        public int Create(Purchase purchase)
        {
            purchase.Id = _purchaseList.Count + 1;
            _purchaseList.Add(purchase);
            return purchase.Id;
        }
    }
}
