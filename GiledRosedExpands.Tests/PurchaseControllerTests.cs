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
    public class When_buying_an_item : WithSubject<PurchaseController>
    {
        static IHttpActionResult result;
        static int _purchaseId = 3;
        Establish context = () =>
        {
            
            The<IPurchaseRepository>().WhenToldTo(x => x.Create(Param.IsAny<Purchase>())).Return(_purchaseId);
        };

        Because of = () =>
        {
            The<IItemRepository>().WhenToldTo(x => x.Get(Param.IsAny<int>())).Return(new Item());
            result = Subject.Post(new PurchaseViewModel { ItemId = 1 });
        };
        It should_return_ok = () =>
        {
            result.ShouldBeOfExactType<CreatedAtRouteNegotiatedContentResult<PurchaseViewModel>>();
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
            The<IItemRepository>().WhenToldTo(x => x.Get(Param.IsAny<int>())).Return((Item) null);
            The<IPurchaseRepository>().WhenToldTo(x => x.Create(Param.IsAny<Purchase>())).Return(_purchaseId);

        };

        Because of = () =>
        {
            result = Subject.Post(new PurchaseViewModel { ItemId = 1 });
        };
        It should_return_bad_request_result = () =>
        {
            result.ShouldBeOfExactType<BadRequestErrorMessageResult>();
        };

        It should_return_the_correct_error_message = () =>
        {
            var response = result as BadRequestErrorMessageResult;
            response.Message.ShouldEqual("The item is no longer available");
        };
    }
}
