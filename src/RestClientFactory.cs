using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Faactory.RestClient;

internal class RestClientFactory : IRestClientFactory
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IEnumerable<IRestPreprocessor> preprocessors;
    private readonly ISerializer serializer;

    public RestClientFactory(
        IHttpClientFactory httpClientFactory,
        IOptions<JsonSerializerOptions> jsonOptionsAccessor,
        IEnumerable<IRestPreprocessor> preprocessors = null,
        ISerializer serializer = null
    )
    {
        this.httpClientFactory = httpClientFactory;
        this.preprocessors = preprocessors ?? Enumerable.Empty<IRestPreprocessor>();
        this.serializer = serializer ?? new Json.DefaultJsonSerializer( jsonOptionsAccessor.Value );
    }

    public IRestClient CreateClient()
        => new RestClient( httpClientFactory.CreateClient(), preprocessors, serializer );

    public IRestClient CreateClient( string name )
        => new RestClient( httpClientFactory.CreateClient( name ), preprocessors, serializer );
}
