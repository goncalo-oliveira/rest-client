using System;
using Microsoft.Extensions.DependencyInjection;

namespace Faactory.RestClient;

internal class RestClientBuilder : IRestClientBuilder
{
    public RestClientBuilder( string name, IServiceCollection serviceCollection )
    {
        Name = name;
        Services = serviceCollection;
    }

    public string Name { get; }
    public IServiceCollection Services { get; }
}
