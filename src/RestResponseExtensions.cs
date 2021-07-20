using System;
using System.Linq;

namespace Faactory.RestClient
{
    public static class RestResponseExtensions
    {
        private static readonly int[] successCodes = new int[]
        {
            200, 201, 202, 203, 204, 205, 206, 207, 208, 226
        };

        private static readonly int[] redirectCodes = new int[]
        {
            300, 301, 302, 303, 304, 305, 307, 308
        };

        public static bool IsOk( this IRestResponse response )
            => response.StatusCode.Equals( 200 );

        public static bool IsSuccess( this IRestResponse response )
            => successCodes.Contains( response.StatusCode );

        //

        public static bool IsRedirection( this IRestResponse response )
            => redirectCodes.Contains( response.StatusCode );

        //

        public static bool IsBadRequest( this IRestResponse response )
            => response.StatusCode.Equals( 400 );

        public static bool IsUnauthorized( this IRestResponse response )
            => response.StatusCode.Equals( 401 );

        public static bool IsForbidden( this IRestResponse response )
            => response.StatusCode.Equals( 403 );

        public static bool IsNotFound( this IRestResponse response )
            => response.StatusCode.Equals( 404 );

        public static bool IsConflict( this IRestResponse response )
            => response.StatusCode.Equals( 409 );

        public static bool IsClientError( this IRestResponse response )
            => ( response.StatusCode >= 400 ) && ( response.StatusCode <= 499 );

        //

        public static bool IsServerError( this IRestResponse response )
            => ( response.StatusCode >= 500 ) && ( response.StatusCode <= 599 );
    }
}
