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
        public static async Task<RestObjectResponse<T>> GetJsonAsync<T>( this RestClient client, string url, CancellationToken cancellationToken = default )
        {
            var response = await client.Configure( options =>
            {
                options.Headers.Accept.Add( new MediaTypeWithQualityHeaderValue( JsonContent.JsonMediaType ) );
            } )
            .GetAsync( url, cancellationToken );

            return RestObjectResponse<T>.Create( response );
        }

        /// <summary>
        /// Sends a POST request to the specified resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PostJsonAsync<T>( this RestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = client.Serializer.Serialize( value );

            return client.Configure( options =>
            {
                options.Headers.Accept.Add( new MediaTypeWithQualityHeaderValue( JsonContent.JsonMediaType ) );
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
        public static Task<RestResponse> PutJsonAsync<T>( this RestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = client.Serializer.Serialize( value );

            return client.Configure( options =>
            {
                options.Headers.Accept.Add( new MediaTypeWithQualityHeaderValue( JsonContent.JsonMediaType ) );
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
        public static Task<RestResponse> PatchJsonAsync<T>( this RestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = client.Serializer.Serialize( value );

            return client.Configure( options =>
            {
                options.Headers.Accept.Add( new MediaTypeWithQualityHeaderValue( JsonContent.JsonMediaType ) );
            } )
            .PatchAsync( url, new JsonContent( content ), cancellationToken );
        }
    }
}
