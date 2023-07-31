using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Xunit;
using System.Threading;

namespace Faactory.RestClient.Tests
{
    public class PreprocessorTests
    {
        internal class InjectPreprocessor : IRestPreprocessor
        {
            public Task ExecuteAsync( HttpRequestMessage request, CancellationToken cancellationToken )
            {
                request.Headers.Add( "X-Test", "Test" );

                return Task.CompletedTask;
            }
        }

        internal class TestPreprocessor : IRestPreprocessor
        {
            public Task ExecuteAsync( HttpRequestMessage request, CancellationToken cancellationToken )
            {
                Assert.Equal( "Test", request.Headers.GetValues( "X-Test" ).First() );

                return Task.CompletedTask;
            }
        }

        private readonly IRestClientFactory clientFactory;

        public PreprocessorTests( IRestClientFactory clientFactory )
        {
            this.clientFactory = clientFactory;
        }

        [Fact]
        public async Task WithDependencyInjectionAsync()
        {
            var client = clientFactory.CreateClient( "jsonplaceholder-preprocessor" );

            Assert.Equal( 2, client.Preprocessors.Count() );

            var response = await client.GetAsync( "todos/1" );

            Assert.Equal( 200, response.StatusCode );
        }
    }
}
