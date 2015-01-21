using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GiledRosedExpands.Repository;
using GiledRosedExpands.ViewModel;

namespace GiledRosedExpands.Controllers
{
    public class ItemController : ApiController
    {
        readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public IHttpActionResult Get()
        {
            return Ok(_itemRepository.GetAll().Select(x => new ItemViewModel{Name = x.Name, Description = x.Description, Price = x.Price}));
        }
    }
}
