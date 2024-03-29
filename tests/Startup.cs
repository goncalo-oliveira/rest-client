using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.DependencyInjection;

namespace Faactory.RestClient.Tests
{
    public class Startup
    {
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddRestClient( "jsonplaceholder", "https://jsonplaceholder.typicode.com" )
                .ConfigurePrimaryHttpMessageHandler( () => new System.Net.Http.HttpClientHandler
                {
                    UseProxy = false
                } );
            services.AddRestClient( "jsonplaceholder-basicauth", "https://jsonplaceholder.typicode.com", httpClient =>
            {
                httpClient.AddBasicAuthentication( AuthorizationTests.username, AuthorizationTests.password );
            } );
            services.AddRestClient( "jsonplaceholder-bearertoken", "https://jsonplaceholder.typicode.com", httpClient =>
            {
                httpClient.AddBearerToken( AuthorizationTests.bearerToken );
            } );

            services.AddRestClient( "jsonplaceholder-preprocessor", "https://jsonplaceholder.typicode.com" )
                .AddPreprocessor<PreprocessorTests.InjectPreprocessor>()
                .AddPreprocessor<PreprocessorTests.TestPreprocessor>();
        }
    }
}
