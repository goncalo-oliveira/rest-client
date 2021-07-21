using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient.Json
{
    /// <summary>
    /// A rest response containing deserialized typed content
    /// </summary>
    /// <typeparam name="T">The type of the deserialized content</typeparam>
    public sealed class RestObjectResponse<T> : IRestResponse<T>, IRestResponse
    {
        internal RestObjectResponse()
        {}

        /// <summary>
        /// Gets the response headers
        /// </summary>
        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; internal set; }

        /// <summary>
        /// Gets the response status code
        /// </summary>
        public int StatusCode { get; internal set; }

        /// <summary>
        /// Gets the response content as a typed object
        /// </summary>
        public T Content { get; internal set; }

        internal static RestObjectResponse<T> Create( RestResponse response )
        {
            var restResponse = new RestObjectResponse<T>
            {
                StatusCode = response.StatusCode,
                Headers = response.Headers
            };

            // only deserialize if the response is a 200 OK
            if ( restResponse.StatusCode == 200 )
            {
                try
                {
                    restResponse.Content = response.JsonSerializer.DeserializeObject<T>( response.Content );
                }
                catch
                {
                    // we are ignoring deserialization errors and returning null
                }
            }

            return ( restResponse );
        }
    }
}
