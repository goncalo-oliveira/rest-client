using System;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient;

/// <summary>
/// A RestClient instance
/// </summary>
public sealed class RestClient : IRestClient
{
    private static Lazy<string> version = new Lazy<string>( 
        () => Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion.ToString()
    );

    public RestClient( HttpClient baseHttpClient, ISerializer contentSerializer = null )
    {
        HttpClient = baseHttpClient;
        Serializer = contentSerializer ?? new Json.DefaultJsonSerializer();

        if ( HttpClient.DefaultRequestHeaders.UserAgent.Count == 0 )
        {
            HttpClient.DefaultRequestHeaders.UserAgent.Add( 
                new System.Net.Http.Headers.ProductInfoHeaderValue( 
                    new System.Net.Http.Headers.ProductHeaderValue( "Faactory.RestClient", version.Value )
                )
            );
        }
    }

    /// <summary>
    /// The underlying HttpClient instance
    /// </summary>
    public HttpClient HttpClient { get; }

    public ISerializer Serializer { get; }

    public IRestClient Configure( Action<RestRequestOptions> configure )
    {
        var options = new RestRequestOptions();

        // add default headers to options
        options.Headers.CopyFrom( HttpClient.DefaultRequestHeaders );

        // apply user configuration
        configure?.Invoke( options );

        // create request
        var request = new RestRequest( this, options );

        return ( request );
    }

    /// <summary>
    /// Sends a GET request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    public Task<RestResponse> GetAsync( string url, CancellationToken cancellationToken = default )
        => this.SendASync( null, HttpMethod.Get, url, null, cancellationToken );

    /// <summary>
    /// Sends a POST request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location</param>
    /// <param name="content">The request body content</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    public Task<RestResponse> PostAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
        => this.SendASync( null, HttpMethod.Post, url, content, cancellationToken );

    /// <summary>
    /// Sends a PUT request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location</param>
    /// <param name="content">The request body content</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    public Task<RestResponse> PutAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
        => this.SendASync( null, HttpMethod.Put, url, content, cancellationToken );

    /// <summary>
    /// Sends a PATCH request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location</param>
    /// <param name="content">The request body content</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    public Task<RestResponse> PatchAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
        => this.SendASync( null, HttpMethod.Patch, url, content, cancellationToken );

    /// <summary>
    /// Sends a DELETE request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    public Task<RestResponse> DeleteAsync( string url, CancellationToken cancellationToken = default )
        => this.SendASync( null, HttpMethod.Delete, url, null, cancellationToken );
}
