using System;
using System.Linq;
using System.Text;

namespace Faactory.RestClient;

public static class RestClientRequestExtensions
{
    /// <summary>
    /// Creates a resource scoped and configurable rest request
    /// </summary>
    /// <param name="url">The request url to scope into</param>
    /// <param name="configure">The action to configure the request</param>
    /// <returns>A pre-configured RestScopedRequest instance</returns>
    public static IRestRequest Configure( this IRestClient client, string url, Action<RestRequestOptions> configure )
    {
        var options = new RestRequestOptions();

        // add default headers to options
        options.Headers.CopyFrom( client.HttpClient.DefaultRequestHeaders );

        var resourceUrl = ResourceUrl.FromString( url );

        // add default query parameters to options
        if ( resourceUrl.QueryParameters.HasKeys() )
        {
            options.QueryParameters = resourceUrl.QueryParameters;
        }

        // apply user configuration
        configure?.Invoke( options );

        // rebuild request url with query parameters from options
        resourceUrl = new ResourceUrl( resourceUrl.Path, options.QueryParameters );

        // create scoped request
        var request = new RestRequest( client, options, resourceUrl );

        return ( request );
    }
}
