using System;

namespace Faactory.RestClient
{
    /// <summary>
    /// A factory abstraction for a component that can create RestClient instances
    ///     with custom configuration for a given logical name
    /// </summary>
    public interface IRestClientFactory
    {
        /// <summary>
        /// Creates and configures a RestClient instance using the default configuration
        /// </summary>
        /// <returns>A RestClient instance</returns>
        IRestClient CreateClient();

        /// <summary>
        /// Creates and configures a RestClient instance using the configuration
        ///     that corresponds to the logical name specified by name
        /// </summary>
        /// <param name="name">The logical name of the client to create</param>
        /// <returns>A RestClient instance</returns>
        IRestClient CreateClient( string name );
    }
}
