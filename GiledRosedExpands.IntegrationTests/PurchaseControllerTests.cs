using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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
using Newtonsoft.Json;
using NUnit.Framework;
using Owin;

namespace GiledRosedExpands.IntegrationTests
{

    [TestFixture]
    public class PurchaseControllerTests : BaseControllerTests
    {
        [Test]
        public async void When_purchasing_an_item()
        {
            
            using (var client = new HttpClient())
            {
                //Arrange
                PurchaseViewModel purchase;
                client.BaseAddress = new Uri(this.baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonFormatter = new JsonMediaTypeFormatter();
                HttpContent content =
                    new ObjectContent<PurchaseViewModel>(
                        new PurchaseViewModel { ItemName = "Item 1", Username = "Peter" }, jsonFormatter);

                //Act
                HttpResponseMessage response = await client.PostAsync("api/purchase/",content);

                //Assert
                The_response_returns_json_result(response);

                The_response_returns_purchase_created(response);

                

                
            }
            
        }

        [Test]
        public async void When_purchasing_a_no_longer_available_item()
        {
            

            using (var client = new HttpClient())
            {
                //Arrange
                string itemNotFound = "Item 12";
                string username = "Peter";
                client.BaseAddress = new Uri(this.baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonFormatter = new JsonMediaTypeFormatter();
                HttpContent content =
                    new ObjectContent<PurchaseViewModel>(
                        new PurchaseViewModel { ItemName = itemNotFound, Username = username }, jsonFormatter);

                //Act
                HttpResponseMessage response = await client.PostAsync("api/purchase/", content);

                //Assert
                The_response_returns_not_found(response);
            }
        }

        [Test]
        public async void When_purchasing_and_not_sending_the_item_name()
        {
            

            using (var client = new HttpClient())
            {
                //Arrange
                string username = "Peter";
                client.BaseAddress = new Uri(this.baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonFormatter = new JsonMediaTypeFormatter();
                HttpContent content =
                    new ObjectContent<PurchaseViewModel>(
                        new PurchaseViewModel { Username = username }, jsonFormatter);

                //Act
                HttpResponseMessage response = await client.PostAsync("api/purchase/", content);

                //Assert
                The_response_returns_bad_request(response);

                The_error_message_indicates_missing_item_name(response);
            }
        }


        static async void The_response_returns_purchase_created(HttpResponseMessage response)
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.NotNull(response.Content);
            Assert.NotNull(response.Content.Headers.ContentType);
            var purchase = await response.Content.ReadAsAsync<PurchaseViewModel>();
            Assert.IsTrue(purchase.PurchaseId == 1);
        }

        async void The_error_message_indicates_missing_item_name(HttpResponseMessage response)
        {
            var value = await response.Content.ReadAsStringAsync();
            var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(value, obj);
            Assert.AreEqual("Item name is required", x.ModelState.First().Value[0]);
        }




    }

}
