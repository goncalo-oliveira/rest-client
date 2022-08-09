using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faactory.RestClient.Json;

namespace Faactory.RestClient
{
    /// <summary>
    /// JSON extensions for the RestClient instance
    /// </summary>
    public static class RestClientJsonExtensions
    {
        /// <summary>
        /// Sends a GET request to the specified resource location and deserializes from the JSON in the response body
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to deserialize to</typeparam>
        /// <returns>The response content if succeeded; default( T ) otherwise.</returns>
        public static async Task<T> GetJsonAsync<T>( this IRestClient client, string url, CancellationToken cancellationToken = default ) where T : class
        {
            var response = await client.Configure( options =>
            {
                options.AddAcceptHeader( new MediaTypeWithQualityHeaderValue( JsonContent.JsonMediaType ) );
            } )
            .GetAsync( url, cancellationToken );

            if ( response.StatusCode == 200 )
            {
                return response.Serializer.Deserialize<T>( response.Content );
            }

            return default( T );
        }

        /// <summary>
        /// Sends a POST request to the specified resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PostJsonAsync<T>( this IRestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = client.Serializer.Serialize( value );

            return client.Configure( options =>
            {
                options.AddAcceptHeader( new MediaTypeWithQualityHeaderValue( JsonContent.JsonMediaType ) );
            } )
            .PostAsync( url, new JsonContent( content ), cancellationToken );
        }

        /// <summary>
        /// Sends a PUT request to the specified resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PutJsonAsync<T>( this IRestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = client.Serializer.Serialize( value );

            return client.Configure( options =>
            {
                options.AddAcceptHeader( new MediaTypeWithQualityHeaderValue( JsonContent.JsonMediaType ) );
            } )
            .PutAsync( url, new JsonContent( content ), cancellationToken );
        }

        /// <summary>
        /// Sends a PATCH request to the specified resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PatchJsonAsync<T>( this IRestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = client.Serializer.Serialize( value );

            return client.Configure( options =>
            {
                options.AddAcceptHeader( new MediaTypeWithQualityHeaderValue( JsonContent.JsonMediaType ) );
            } )
            .PatchAsync( url, new JsonContent( content ), cancellationToken );
        }
    }
}
