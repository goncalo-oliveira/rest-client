using System;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Faactory.RestClient;

internal class RestClientFactory : IRestClientFactory
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ISerializer serializer;

    public RestClientFactory(
        IHttpClientFactory httpClientFactory,
        IOptions<JsonSerializerOptions> jsonOptionsAccessor,
        ISerializer serializer = null
    )
    {
        this.httpClientFactory = httpClientFactory;
        this.serializer = serializer ?? new Json.DefaultJsonSerializer( jsonOptionsAccessor.Value );
    }

    public IRestClient CreateClient()
        => new RestClient( httpClientFactory.CreateClient(), serializer );

    public IRestClient CreateClient( string name )
        => new RestClient( httpClientFactory.CreateClient( name ), serializer );
}
