using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient;

/// <summary>
/// An IRestClient instance
/// </summary>
public interface IRestClient
{
    /// <summary>
    /// The underlying HttpClient instance
    /// </summary>
    HttpClient HttpClient { get; }

    /// <summary>
    /// A collection of preprocessors
    /// </summary>
    System.Collections.Generic.IEnumerable<IRestPreprocessor> Preprocessors { get; }

    /// <summary>
    /// The configured serializer
    /// </summary>
    ISerializer Serializer { get; }

    IRestClient Configure( Action<RestRequestOptions> configure );

    /// <summary>
    /// Sends a GET request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    Task<RestResponse> GetAsync( string url, CancellationToken cancellationToken = default );

    /// <summary>
    /// Sends a POST request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location</param>
    /// <param name="content">The request body content</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    Task<RestResponse> PostAsync( string url, HttpContent content, CancellationToken cancellationToken = default );

    /// <summary>
    /// Sends a PUT request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location</param>
    /// <param name="content">The request body content</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    Task<RestResponse> PutAsync( string url, HttpContent content, CancellationToken cancellationToken = default );

    /// <summary>
    /// Sends a PATCH request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location</param>
    /// <param name="content">The request body content</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    Task<RestResponse> PatchAsync( string url, HttpContent content, CancellationToken cancellationToken = default );

    /// <summary>
    /// Sends a DELETE request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    Task<RestResponse> DeleteAsync( string url, CancellationToken cancellationToken = default );
}
