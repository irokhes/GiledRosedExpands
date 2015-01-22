using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using GiledRosedExpands.Controllers;
using GiledRosedExpands.Domain.Repositories;
using GiledRosedExpands.ViewModel;
using Machine.Fakes;
using Machine.Specifications;
using GiledRosedExpands.Domain.Models;

namespace GiledRosedExpands.Tests
{
    public class When_buying_an_item : WithSubject<PurchaseController>
    {
        static IHttpActionResult result;
        static int _purchaseId = 3;
        static Item _item;
        static string _fixieBike;
        static string _username;

        Establish context = () =>
        {
            _fixieBike = "fixie bike";
            _username = "Joseph";
            _item = new Item
            {
                Name = "Item",
                Description = "desc item",
                Price = 9
            };
            The<IItemRepository>().WhenToldTo(x => x.Get(Param.IsAny<string>())).Return(_item);
            The<IPurchaseRepository>().WhenToldTo(x => x.Create(Param.IsAny<Purchase>())).Return(_purchaseId);
        };

        Because of = () =>
        {
            
            result = Subject.Post(new PurchaseViewModel { ItemName = _fixieBike, Username = _username});
        };
        It should_return_ok = () =>
        {
            result.ShouldBeOfExactType<CreatedAtRouteNegotiatedContentResult<PurchaseViewModel>>();
        };

        It should_store_the_purchase_correctly = () =>
        {
            The<IPurchaseRepository>().WasToldTo(x => x.Create(Param<Purchase>.Matches(p => p.Item == _item)));
            The<IPurchaseRepository>().WasToldTo(x => x.Create(Param<Purchase>.Matches(p => p.Username == _username)));
        };

        It should_return_the_route_to_the_new_purchase = () =>
        {
            var response = result as CreatedAtRouteNegotiatedContentResult<PurchaseViewModel>;
            response.RouteName.ShouldEqual("DefaultApi");
            response.RouteValues["id"].ShouldEqual(_purchaseId);
        };

        
    }

    public class When_bying_an_item_is_not_longer_available : WithSubject<PurchaseController>
    {
        static IHttpActionResult result;
        static int _purchaseId = 3;
        Establish context = () =>
        {
            The<IItemRepository>().WhenToldTo(x => x.Get(Param.IsAny<string>())).Return((Item) null);
            The<IPurchaseRepository>().WhenToldTo(x => x.Create(Param.IsAny<Purchase>())).Return(_purchaseId);

        };

        Because of = () =>
        {
            result = Subject.Post(new PurchaseViewModel { ItemName = "XP Book" });
        };
        It should_return_bad_request_result = () =>
        {
            result.ShouldBeOfExactType<NotFoundResult>();
        };
    }

    public class When_bying_an_item_and_username_is_not_provided : WithSubject<PurchaseController>
    {
        static IHttpActionResult result;
        static int _purchaseId = 3;
        Establish context = () =>
        {
            var item = new Item
            {
                Name = "Item",
                Description = "desc item",
                Price = 9
            };
            The<IItemRepository>().WhenToldTo(x => x.Get(Param.IsAny<string>())).Return(item);
            The<IPurchaseRepository>().WhenToldTo(x => x.Create(Param.IsAny<Purchase>())).Return(_purchaseId);
            Subject.ModelState.AddModelError("key", "Username is required");

        };

        Because of = () =>
        {
            result = Subject.Post(new PurchaseViewModel { ItemName = "XP Book" });
            
        };
        It should_return_bad_request_result = () =>
        {
            result.ShouldBeOfExactType<InvalidModelStateResult>();
        };

        It should_return_the_correct_error_message = () =>
        {
            var response = result as InvalidModelStateResult;
            response.ModelState["key"].Errors[0].ErrorMessage.ShouldEqual("Username is required");
        };
    }
}
