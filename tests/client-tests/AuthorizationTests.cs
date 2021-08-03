using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Xunit;

namespace Faactory.RestClient.Tests
{
    public class AuthorizationTests
    {
        internal const string username = "username";
        internal const string password = "password";
        internal const string bearerToken = "29773b8c-6649-4266-a804-d11a124941cc";

        private readonly IRestClientFactory clientFactory;

        public AuthorizationTests( IRestClientFactory clientFactory )
        {
            this.clientFactory = clientFactory;
        }

        [Fact]
        public void TestBasicAuthentication()
        {
            var httpClient = new HttpClient();

            httpClient.AddBasicAuthentication( username, password );

            Assert.Equal( "Basic", httpClient.DefaultRequestHeaders.Authorization.Scheme );
            Assert.Equal( "dXNlcm5hbWU6cGFzc3dvcmQ=", httpClient.DefaultRequestHeaders.Authorization.Parameter );

            var restClient = clientFactory.CreateClient( "jsonplaceholder-basicauth" );

            Assert.Equal( "Basic", restClient.HttpClient.DefaultRequestHeaders.Authorization.Scheme );
            Assert.Equal( "dXNlcm5hbWU6cGFzc3dvcmQ=", restClient.HttpClient.DefaultRequestHeaders.Authorization.Parameter );
        }

        [Fact]
        public void TestBearerToken()
        {
            var httpClient = new HttpClient();

            httpClient.AddBearerToken( bearerToken );

            Assert.Equal( "Bearer", httpClient.DefaultRequestHeaders.Authorization.Scheme );
            Assert.Equal( bearerToken, httpClient.DefaultRequestHeaders.Authorization.Parameter );

            var restClient = clientFactory.CreateClient( "jsonplaceholder-bearertoken" );

            Assert.Equal( "Bearer", restClient.HttpClient.DefaultRequestHeaders.Authorization.Scheme );
            Assert.Equal( bearerToken, restClient.HttpClient.DefaultRequestHeaders.Authorization.Parameter );
        }
    }
}
