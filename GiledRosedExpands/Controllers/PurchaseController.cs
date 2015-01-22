using System;
using System.Web.Http;
using GiledRosedExpands.Domain.Models;
using GiledRosedExpands.Domain.Repositories;
using GiledRosedExpands.ViewModel;

namespace GiledRosedExpands.Controllers
{
    public class PurchaseController : ApiController
    {
        readonly IPurchaseRepository _purchaseRepository;
        readonly IItemRepository _itemRepository;
        const string RouteName = "DefaultApi";

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
                return BadRequest(ModelState);
            }
            var item = _itemRepository.Get(purchaseViewModel.ItemName);
            if (item == null)
            {
                return NotFound();
            }
            var purchase = new Purchase
            {
                Item = item,
                Date = DateTime.Now,
                Username = purchaseViewModel.Username
            };
            purchaseViewModel.PurchaseId = _purchaseRepository.Create(purchase);
            
            return CreatedAtRoute(RouteName, new {id = purchaseViewModel.PurchaseId}, purchaseViewModel);
        }
    }
}