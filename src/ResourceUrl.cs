using System;
using System.Collections.Specialized;
using System.Linq;

namespace Faactory.RestClient
{
    /// <summary>
    /// A resource url for a rest request
    /// </summary>
    public sealed class ResourceUrl
    {
        public ResourceUrl( string urlPath, NameValueCollection? urlQuery = null )
        {
            Path = urlPath;
            QueryParameters = urlQuery ?? new NameValueCollection();
        }

        /// <summary>
        /// Gets the path of the resource url
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets the query parameters of the resource url
        /// </summary>
        public NameValueCollection QueryParameters { get; }

        public static implicit operator string( ResourceUrl url )
        {
            return url.ToString();
        }

        public static implicit operator Uri( ResourceUrl url )
        {
            return new Uri( url.ToString(), UriKind.RelativeOrAbsolute );
        }

        public override string ToString()
        {
            if ( !QueryParameters.HasKeys() )
            {
                return ( Path );
            }

            var queryString = QueryParameters.AllKeys.Select( key => 
                string.Concat( key, "=", System.Web.HttpUtility.UrlEncode( QueryParameters[key] ) ) );

            var url = string.Concat( Path, "?", string.Join( '&', queryString ) );

            return ( url );
        }
    
        /// <summary>
        /// Creates a ResourceUrl from the given url string
        /// </summary>
        /// <param name="url">The url string to create the ResourceUrl from</param>
        /// <returns>A ResourceUrl instance</returns>
        public static ResourceUrl FromString( string url )
        {
            var path = url;
            NameValueCollection? queryParameters = null;

            if ( path.Contains( '?' ) )
            {
                int queryIndex = path.IndexOf( '?' );
                queryParameters = System.Web.HttpUtility.ParseQueryString( path[queryIndex..] );
                path = path[..queryIndex];
            }

            return new ResourceUrl( path, queryParameters );
        }

        /// <summary>
        /// Creates a ResourceUrl from the given Uri
        /// </summary>
        /// <param name="uri">The Uri to create the ResourceUrl from</param>
        /// <returns>A ResourceUrl instance</returns>
        public static ResourceUrl FromUri( Uri uri )
            => FromString( uri.ToString() );
    }
}
