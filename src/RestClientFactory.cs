using System;
using System.Net.Http;

namespace Faactory.RestClient
{
    internal class RestClientFactory : IRestClientFactory
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ISerializer serializer;

        public RestClientFactory( IHttpClientFactory httpClientFactory, ISerializer serializer = null )
        {
            this.httpClientFactory = httpClientFactory;
            this.serializer = serializer;
        }

        public RestClient CreateClient()
        {
            return new RestClient( httpClientFactory.CreateClient(), serializer );
        }

        public RestClient CreateClient( string name )
        {
            var httpClient = httpClientFactory.CreateClient( name );

            return new RestClient( httpClient, serializer );
        }
    }
}
