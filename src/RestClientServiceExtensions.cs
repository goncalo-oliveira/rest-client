using System;
using System.Linq;
using System.Net.Http;
using Faactory.RestClient;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RestClientServiceExtensions
    {
        public static IServiceCollection AddRestClient( this IServiceCollection services )
        {
            services.AddRestClientFactory();
            services.AddHttpClient();

            return ( services );
        }

        public static IServiceCollection AddRestClient( this IServiceCollection services, string name, string url )
        {
            services.AddRestClientFactory();
            services.AddHttpClient( name, httpClient =>
            {
                httpClient.BaseAddress = new Uri( url.TrimEnd( '/' ) );
            } );

            return ( services );
        }

        public static IServiceCollection AddRestClient( this IServiceCollection services, string name, Action<HttpClient> configureClient )
        {
            services.AddRestClientFactory();
            services.AddHttpClient( name, configureClient );

            return ( services );
        }

        public static IServiceCollection AddRestClient( this IServiceCollection services, string name, string url, Action<HttpClient> configureClient )
        {
            services.AddRestClientFactory();
            services.AddHttpClient( name, httpClient =>
            {
                httpClient.BaseAddress = new Uri( url.TrimEnd( '/' ) );

                configureClient?.Invoke( httpClient );
            } );

            return ( services );
        }

        public static IServiceCollection AddRestClient( this IServiceCollection services, string name, Action<IServiceProvider, HttpClient> configureClient )
        {
            services.AddRestClientFactory();
            services.AddHttpClient( name, configureClient );

            return ( services );
        }

        private static IServiceCollection AddRestClientFactory( this IServiceCollection services )
        {
            services.TryAddSingleton<IRestClientFactory, RestClientFactory>();

            return ( services );
        }
    }
}
