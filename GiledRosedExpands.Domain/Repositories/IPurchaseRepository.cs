using GiledRosedExpands.Domain.Models;

namespace GiledRosedExpands.Domain.Repositories
{
    public interface IPurchaseRepository
    {
        Purchase Get(int id);
        int Create(Purchase purchase);
    }
}