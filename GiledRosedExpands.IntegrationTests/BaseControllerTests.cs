using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.SelfHost;
using NUnit.Framework;

namespace GiledRosedExpands.IntegrationTests
{
    public class BaseControllerTests
    {
        protected HttpSelfHostServer server = null;
        protected string baseAddress = null;

        // Setup
        [SetUp]
        public void SetUpTests()
        {
            NinjectWebCommon.Start();
            baseAddress = string.Format("http://{0}:9090", Environment.MachineName);

            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(this.baseAddress);
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
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

        protected void The_response_returns_not_found(HttpResponseMessage response)
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        protected void The_response_returns_bad_request(HttpResponseMessage response)
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        protected void The_response_returns_json_result(HttpResponseMessage response)
        {
            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
        }
    }
}
