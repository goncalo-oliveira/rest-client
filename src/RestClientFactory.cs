using System;
using System.Net.Http;

namespace Faactory.RestClient
{
    internal class RestClientFactory : IRestClientFactory
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly Json.IJsonSerializer jsonSerializer;

        public RestClientFactory( IHttpClientFactory httpClientFactory, Json.IJsonSerializer jsonSerializer = null )
        {
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializer = jsonSerializer;
        }

        public RestClient CreateClient()
        {
            return new RestClient( httpClientFactory.CreateClient(), jsonSerializer );
        }

        public RestClient CreateClient( string name )
        {
            var httpClient = httpClientFactory.CreateClient( name );

            return new RestClient( httpClient, jsonSerializer );
        }
    }
}
