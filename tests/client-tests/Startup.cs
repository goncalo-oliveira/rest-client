using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.DependencyInjection;

namespace Faactory.RestClient.Tests
{
    public class Startup
    {
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddRestClient( "jsonplaceholder", "https://jsonplaceholder.typicode.com" );
            services.AddRestClient( "jsonplaceholder-basicauth", "https://jsonplaceholder.typicode.com", httpClient =>
            {
                httpClient.AddBasicAuthentication( AuthorizationTests.username, AuthorizationTests.password );
            } );
            services.AddRestClient( "jsonplaceholder-bearertoken", "https://jsonplaceholder.typicode.com", httpClient =>
            {
                httpClient.AddBearerToken( AuthorizationTests.bearerToken );
            } );
        }
    }
}
