using System.Net.Http;
using Alert_to_Care;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Alert_To_Care.Integration.Tests
{
    internal class TestContext
    {
        private TestServer _server;

        public TestContext()
        {
            SetupClient();
        }

        public HttpClient Client { get; private set; }

        private void SetupClient()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
        }
    }
}