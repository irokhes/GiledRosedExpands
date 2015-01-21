using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using GiledRosedExpands.Controllers;
using GiledRosedExpands.Models;
using GiledRosedExpands.Repository;
using GiledRosedExpands.ViewModel;
using Machine.Fakes;
using Machine.Specifications;

namespace GiledRosedExpands.Tests
{
    public class When_getting_all_items : WithSubject<ItemController>
    {
        static IHttpActionResult result;
        Establish that = () =>
        {
            The<IItemRepository>().WhenToldTo(x => x.GetAll()).Return(new List<Item>{
                new Item{Name = "item1", Description = "desc item 1", Price = 12}
            });
        };

        Because of = () =>
        {
            result = Subject.Get();
        };

        It should_return_an_ok_response = () =>
        {
            result.ShouldBeOfExactType<OkNegotiatedContentResult<IEnumerable<ItemViewModel>>>();
        };

        It should_return_at_least_one_item = () =>
        {
            var response = result as OkNegotiatedContentResult<IEnumerable<ItemViewModel>>;
            response.Content.Count().ShouldBeGreaterThan(0);
        };
    }

   
}
