using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiledRosedExpands.Domain.Models;

namespace GiledRosedExpands.Infrastructure.Repositories
{
    class Context : DbContext
    {
        public Context()
            : base("MyDb")
        { }

        public IDbSet<Item> PersonSet { get; set; }
        public IDbSet<Purchase> AddressSet { get; set; }
    }
}
