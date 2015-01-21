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

        public PurchaseController(IPurchaseRepository purchaseRepository, IItemRepository itemRepository)
        {
            _purchaseRepository = purchaseRepository;
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_purchaseRepository.Get(id));
        }

        [HttpPost]
        public IHttpActionResult Post(PurchaseViewModel purchaseViewModel)
        {
            if (!ModelState.IsValid)
            {
                //Return error
                BadRequest(ModelState);
            }
            var item = _itemRepository.Get(purchaseViewModel.ItemName);
            if (item == null)
            {
                return BadRequest("The item is no longer available");
            }
            var purchase = new Purchase
            {
                Item = item,
                Date = DateTime.Now,
                Username = purchaseViewModel.Username
            };
            purchaseViewModel.PurchaseId = _purchaseRepository.Create(purchase);
            return CreatedAtRoute("DefaultApi", new {id = 3}, purchaseViewModel);
        }
    }
}