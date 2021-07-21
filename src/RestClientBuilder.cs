using System;
using Microsoft.Extensions.DependencyInjection;

namespace Faactory.RestClient
{
    internal class RestClientBuilder : IRestClientBuilder
    {
        public RestClientBuilder( IServiceCollection serviceCollection )
        {
            Services = serviceCollection;
        }

        public IServiceCollection Services { get; }
    }
}
