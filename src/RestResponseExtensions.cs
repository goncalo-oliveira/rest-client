using System;
using System.Linq;

namespace Faactory.RestClient
{
    /// <summary>
    /// Extensions for the rest responses
    /// </summary>
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

        /// <summary>
        /// Gets if the response status code is 200 OK
        /// </summary>
        /// <returns>True if the response status code is 200 OK, false otherwise</returns>
        public static bool IsOk( this IRestResponse response )
            => response.StatusCode.Equals( 200 );

        /// <summary>
        /// Gets if the response status code is 201 Created
        /// </summary>
        /// <returns>True if the response status code is 201 Created, false otherwise</returns>
        public static bool IsCreated( this IRestResponse response )
            => response.StatusCode.Equals( 201 );

        /// <summary>
        /// Gets if the response status code is 202 Accepted
        /// </summary>
        /// <returns>True if the response status code is 202 Accepted, false otherwise</returns>
        public static bool IsAccepted( this IRestResponse response )
            => response.StatusCode.Equals( 202 );

        /// <summary>
        /// Gets if the response status code is 204 No Content
        /// </summary>
        /// <returns>True if the response status code is 204 No Content, false otherwise</returns>
        public static bool IsNoContent( this IRestResponse response )
            => response.StatusCode.Equals( 204 );

        /// <summary>
        /// Gets if the response status code is successful (2xx)
        /// </summary>
        /// <returns>True if the response status code is successful, false otherwise</returns>
        public static bool IsSuccess( this IRestResponse response )
            => successCodes.Contains( response.StatusCode );

        //

        /// <summary>
        /// Gets if the response status code is a redirection (3xx)
        /// </summary>
        /// <returns>True if the response status code is a redirection, false otherwise</returns>
        public static bool IsRedirection( this IRestResponse response )
            => redirectCodes.Contains( response.StatusCode );

        //

        /// <summary>
        /// Gets if the response status code is 400 Bad Request
        /// </summary>
        /// <returns>True if the response status code is 400 Bad Request, false otherwise</returns>
        public static bool IsBadRequest( this IRestResponse response )
            => response.StatusCode.Equals( 400 );

        /// <summary>
        /// Gets if the response status code is 401 Unauthorized
        /// </summary>
        /// <returns>True if the response status code is 401 Unauthorized, false otherwise</returns>
        public static bool IsUnauthorized( this IRestResponse response )
            => response.StatusCode.Equals( 401 );

        /// <summary>
        /// Gets if the response status code is 403 Forbidden
        /// </summary>
        /// <returns>True if the response status code is 403 Forbidden, false otherwise</returns>
        public static bool IsForbidden( this IRestResponse response )
            => response.StatusCode.Equals( 403 );

        /// <summary>
        /// Gets if the response status code is 404 Not Found
        /// </summary>
        /// <returns>True if the response status code is 404 Not Found, false otherwise</returns>
        public static bool IsNotFound( this IRestResponse response )
            => response.StatusCode.Equals( 404 );

        /// <summary>
        /// Gets if the response status code is 409 Conflict
        /// </summary>
        /// <returns>True if the response status code is 409 Conflict, false otherwise</returns>
        public static bool IsConflict( this IRestResponse response )
            => response.StatusCode.Equals( 409 );

        /// <summary>
        /// Gets if the response status code is a client error (4xx)
        /// </summary>
        /// <returns>True if the response status code is a client error, false otherwise</returns>
        public static bool IsClientError( this IRestResponse response )
            => ( response.StatusCode >= 400 ) && ( response.StatusCode <= 499 );

        //

        /// <summary>
        /// Gets if the response status code is a server error (5xx)
        /// </summary>
        /// <returns>True if the response status code is a server error, false otherwise</returns>
        public static bool IsServerError( this IRestResponse response )
            => ( response.StatusCode >= 500 ) && ( response.StatusCode <= 599 );
    }
}
