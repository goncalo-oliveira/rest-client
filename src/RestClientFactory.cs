using System;
using System.Net.Http;

namespace Faactory.RestClient
{
    internal class RestClientFactory : IRestClientFactory
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RestClientFactory( IHttpClientFactory httpClientFactory )
        {
            this.httpClientFactory = httpClientFactory;
        }

        public RestClient CreateClient()
        {
            return new RestClient( httpClientFactory.CreateClient() );
        }

        public RestClient CreateClient( string name )
        {
            var httpClient = httpClientFactory.CreateClient( name );

            return new RestClient( httpClient );
        }
    }
}
