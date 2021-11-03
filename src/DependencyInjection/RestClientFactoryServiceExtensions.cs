using System;
using System.Linq;
using System.Net.Http;
using Faactory.RestClient;
using Faactory.RestClient.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions methods to configure an IServiceCollection for IRestClientFactory
    /// </summary>
    public static class RestClientFactoryServiceExtensions
    {
        /// <summary>
        /// Adds the IRestClientFactory and related services to the IServiceCollection
        /// </summary>
        /// <returns>An IRestClientBuilder that can be used to further configure the RestClient services</returns>
        public static IRestClientBuilder AddRestClient( this IServiceCollection services )
        {
            services.AddRestClientFactory();
            services.AddHttpClient();

            return new RestClientBuilder( services );
        }

        /// <summary>
        /// Adds the IRestClientFactory and related services to the IServiceCollection and configures a named RestClient
        /// </summary>
        /// <param name="name">The name of the RestClient to configure</param>
        /// <param name="url">The base url of the RestClient to configure</param>
        /// <returns>An IRestClientBuilder that can be used to further configure the RestClient services</returns>
        public static IRestClientBuilder AddRestClient( this IServiceCollection services, string name, string url )
        {
            services.AddRestClientFactory();
            services.AddHttpClient( name, httpClient =>
            {
                httpClient.BaseAddress = new UriBuilder( url )
                    .EnsurePathTrailingSlash()
                    .Uri;
            } );

            return new RestClientBuilder( services );
        }

        /// <summary>
        /// Adds the IRestClientFactory and related services to the IServiceCollection and configures a named RestClient
        /// </summary>
        /// <param name="name">The name of the RestClient to configure</param>
        /// <param name="configureClient">A delegate to configure the underlying System.Net.Http.HttpClient</param>
        /// <returns>An IRestClientBuilder that can be used to further configure the RestClient services</returns>
        public static IRestClientBuilder AddRestClient( this IServiceCollection services, string name, Action<HttpClient> configureClient )
        {
            services.AddRestClientFactory();
            services.AddHttpClient( name, configureClient );

            return new RestClientBuilder( services );
        }

        /// <summary>
        /// Adds the IRestClientFactory and related services to the IServiceCollection and configures a named RestClient
        /// </summary>
        /// <param name="name">The name of the RestClient to configure</param>
        /// <param name="url">The base url of the RestClient to configure</param>
        /// <param name="configureClient">A delegate to configure the underlying System.Net.Http.HttpClient</param>
        /// <returns>An IRestClientBuilder that can be used to further configure the RestClient services</returns>
        public static IRestClientBuilder AddRestClient( this IServiceCollection services, string name, string url, Action<HttpClient> configureClient )
        {
            services.AddRestClientFactory();
            services.AddHttpClient( name, httpClient =>
            {
                httpClient.BaseAddress = new UriBuilder( url )
                    .EnsurePathTrailingSlash()
                    .Uri;

                configureClient?.Invoke( httpClient );
            } );

            return new RestClientBuilder( services );
        }

        /// <summary>
        /// Adds the IRestClientFactory and related services to the IServiceCollection and configures a named RestClient
        /// </summary>
        /// <param name="name">The name of the RestClient to configure</param>
        /// <param name="configureClient">A delegate to configure the underlying System.Net.Http.HttpClient</param>
        /// <returns>An IRestClientBuilder that can be used to further configure the RestClient services</returns>
        public static IRestClientBuilder AddRestClient( this IServiceCollection services, string name, Action<IServiceProvider, HttpClient> configureClient )
        {
            services.AddRestClientFactory();
            services.AddHttpClient( name, configureClient );

            return new RestClientBuilder( services );
        }

        private static IServiceCollection AddRestClientFactory( this IServiceCollection services )
        {
            services.TryAddSingleton<IRestClientFactory, RestClientFactory>();

            return ( services );
        }
    }
}
