using System.Web.Http;
using GiledRosedExpands.Models;
using GiledRosedExpands.Repository;
using GiledRosedExpands.ViewModel;

namespace GiledRosedExpands.Controllers
{
    public class PurchaseController : ApiController
    {
        readonly IPurchaseRepository _purchaseRepository;
        readonly IItemRepository _itemRepository;

        public PurchaseController(IPurchaseRepository purchaseRepository, IItemRepository itemRepository)
        {
            _purchaseRepository = purchaseRepository;
            _itemRepository = itemRepository;
        }

        [HttpPost]
        public IHttpActionResult Post(PurchaseViewModel purchaseViewModel)
        {
            if (!ModelState.IsValid)
            {
                //Return error
                BadRequest(ModelState);
            }
            var item = _itemRepository.Get(purchaseViewModel.ItemId);
            if (item == null)
            {
                return BadRequest("The item is no longer available");
            }
            var purchase = new Purchase
            {
                
            };
            purchaseViewModel.PurchaseId = _purchaseRepository.Create(purchase);
            return CreatedAtRoute("DefaultApi", new {id = 3}, purchaseViewModel);
        }
    }
}