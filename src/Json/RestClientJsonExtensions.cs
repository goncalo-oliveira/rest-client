using System;
using System.Net.Http;
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
        /// Sends a GET request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public static async Task<RestObjectResponse<T>> GetJsonAsync<T>( this RestClient client, string url, CancellationToken cancellationToken = default )
        {
            var response = await client.GetAsync( url, cancellationToken );

            return RestObjectResponse<T>.Create( response );
        }

        /// <summary>
        /// Sends a POST request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="value">The value to serialize into the request body</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The value type</typeparam>
        public static Task<RestResponse> PostJsonAsync<T>( this RestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = client.JsonSerializer.SerializeObject( value );
            
            return client.PostAsync( url, content, cancellationToken );
        }

        /// <summary>
        /// Sends a PUT request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="value">The value to serialize into the request body</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The value type</typeparam>
        public static Task<RestResponse> PutJsonAsync<T>( this RestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = client.JsonSerializer.SerializeObject( value );

            return client.PutAsync( url, content, cancellationToken );
        }

        /// <summary>
        /// Sends a PATCH request to the specified resource location
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="value">The value to serialize into the request body</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The value type</typeparam>
        public static Task<RestResponse> PatchJsonAsync<T>( this RestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = client.JsonSerializer.SerializeObject( value );

            return client.PatchAsync( url, content, cancellationToken );
        }
    }
}
