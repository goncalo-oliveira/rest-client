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
        }
    }
}
