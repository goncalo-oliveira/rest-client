using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient;

/// <summary>
/// An scoped client instance
/// </summary>
public interface IRestRequest
{
    IRestClient Client { get; }

    IRestRequest Configure( Action<RestRequestOptions> configure );

    Task<RestResponse> GetAsync( CancellationToken cancellationToken = default );
    Task<RestResponse> PostAsync( HttpContent content, CancellationToken cancellationToken = default );
    Task<RestResponse> PutAsync( HttpContent content, CancellationToken cancellationToken = default );
    Task<RestResponse> PatchAsync( HttpContent content, CancellationToken cancellationToken = default );
    Task<RestResponse> DeleteAsync( CancellationToken cancellationToken = default );
}
