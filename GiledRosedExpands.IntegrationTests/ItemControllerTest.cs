using System;
using System.Collections.Generic;
using System.Linq;
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
using GiledRosedExpands.ViewModel;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Owin;

namespace GiledRosedExpands.IntegrationTests
{
   

    [TestFixture]
    public class ItemControllerTest 
    {
        [Test]
        public async void PostTest()
        {
            

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await client.GetAsync("api/item/");
                if (response.IsSuccessStatusCode)
                {
                    var items =  await response.Content.ReadAsAsync<List<ItemViewModel>>();
                    
                }

                Assert.NotNull(response.Content);
                Assert.NotNull(response.Content.Headers.ContentType);
                
            }

            
        }

        [Test]
        public void GetTest()
        {
            string expectedResponse = "<?xml version='1.0' encoding='utf-8'?><string>Hello</string>".Replace("'", "\"");

            HttpClient httpClient = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(this.baseAddress + "/api/item/");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            request.Method = HttpMethod.Get;

            HttpResponseMessage response = httpClient.SendAsync(request).Result;

            Debug.Write(response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.NotNull(response.Content);
            Assert.NotNull(response.Content.Headers.ContentType);
            
        }

        private HttpSelfHostServer server = null;
        private string baseAddress = null;

        // Setup
        [SetUp]
        public void SetUpTests()
        {
            NinjectWebCommon.Start();
            baseAddress = string.Format("http://{0}:9090", Environment.MachineName);

            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(this.baseAddress);
            config.Routes.MapHttpRoute("API Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            config.DependencyResolver = GlobalConfiguration.Configuration.DependencyResolver;
            
            server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
        }

        //tear down
        [TearDown]
        public void Dispose()
        {
            if (server != null)
            {
                server.CloseAsync().Wait();
            }
        }
    }
    


    [TestFixture]
    class ItemControllerTestNotWorking : BaseControllerTest
    {
        
        [Test]
        [Description("This test attempts to retrieve a known item collection.")]
        public void Get_item_list_With_Success()
        {
            var client = new HttpClient(_httpServer);

            var request = CreateRequest(string.Format("api/item/"), "application/json", HttpMethod.Get);
            using (HttpResponseMessage response = client.SendAsync(request, new CancellationTokenSource().Token).Result)
            {
                Assert.NotNull(response.Content);
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);

                var result = response.Content.ReadAsAsync<List<ItemViewModel>>().Result;
                Assert.NotNull(result);

                Assert.AreEqual("Item 1", result.First().Name);
                Assert.AreEqual("Description item 1", result.First().Description);
                Assert.That(response.IsSuccessStatusCode);
            }

            request.Dispose();

        }
    }
}
