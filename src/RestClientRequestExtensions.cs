using System;
using System.Linq;
using System.Text;

namespace Faactory.RestClient
{
    public static class RestClientRequestExtensions
    {
        public static RestRequest Configure( this RestClient client, string url, Action<RestRequestOptions> configure )
        {
            var options = new RestRequestOptions();

            // add default headers to options
            options.Headers.CopyFrom( client.HttpClient.DefaultRequestHeaders );

            // add default query parameters to options
            if ( url.Contains( "?" ) )
            {
                // copy query parameters to options
                var uri = new Uri( url );

                options.QueryParameters = System.Web.HttpUtility.ParseQueryString( uri.Query );
            }

            configure?.Invoke( options );

            // rebuild request url with query parameters from options
            if ( options.QueryParameters.HasKeys() )
            {
                if ( url.Contains( '?' ) )
                {
                    url = url.Substring( 0, url.IndexOf( '?' ) );
                }

                var queryString = options.QueryParameters.AllKeys.Select( key => 
                    string.Concat( key, "=", System.Web.HttpUtility.UrlEncode( options.QueryParameters[key] ) ) );

                url = string.Concat( url, "?", string.Join( '&', queryString ) );
            }

            var request = new RestRequest( client, url, options );

            return ( request );
        }
    }
}
