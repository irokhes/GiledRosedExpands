using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;
using GiledRosedExpands.Controllers;
using GiledRosedExpands.Domain.Models;
using GiledRosedExpands.ViewModel;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Owin;

namespace GiledRosedExpands.IntegrationTests
{
   

    [TestFixture]
    public class ItemControllerTests : BaseControllerTests
    {
        [Test]
        public async void When_getting_item_collection()
        {
            using (var client = new HttpClient())
            {
                //Arrange
                client.BaseAddress = new Uri(this.baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Act
                HttpResponseMessage response = await client.GetAsync("api/item/");


                //Assert
                The_response_returns_json_result(response);

                The_response_returns_ok(response);
                
                await A_list_of_items_is_available(response);
            }
        }

        async Task A_list_of_items_is_available(HttpResponseMessage response)
        {
            var items = await response.Content.ReadAsAsync<List<ItemViewModel>>();
            Assert.IsTrue(items.Count == 3);
        }

        void The_response_returns_ok(HttpResponseMessage response)
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.NotNull(response.Content);
            Assert.NotNull(response.Content.Headers.ContentType);
        }
    }
   
}
