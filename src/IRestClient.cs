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
    HttpClient HttpClient { get; }
    ISerializer Serializer { get; }

    IRestClient Configure( Action<RestRequestOptions> configure );

    Task<RestResponse> GetAsync( string url, CancellationToken cancellationToken = default );
    Task<RestResponse> PostAsync( string url, HttpContent content, CancellationToken cancellationToken = default );
    Task<RestResponse> PutAsync( string url, HttpContent content, CancellationToken cancellationToken = default );
    Task<RestResponse> PatchAsync( string url, HttpContent content, CancellationToken cancellationToken = default );
    Task<RestResponse> DeleteAsync( string url, CancellationToken cancellationToken = default );
}
