using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Faactory.RestClient.Json;

namespace Faactory.RestClient
{
    public static class RestRequestJsonExtensions
    {
        /// <summary>
        /// Sends a GET request to the pre-configured resource location and deserializes from the JSON in the response body
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to deserialize to</typeparam>
        /// <returns>A RestObjectResponse instance with the deserialized object</returns>
        public static async Task<RestObjectResponse<T>> GetJsonAsync<T>( this RestRequest request, CancellationToken cancellationToken = default )
        {
            var response = await request.GetAsync( cancellationToken );

            return RestObjectResponse<T>.Create( response );
        }

        /// <summary>
        /// Sends a GET request to the specified resource location and deserializes from the JSON in the response body
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to deserialize to</typeparam>
        /// <returns>A RestObjectResponse instance with the deserialized object</returns>
        public static async Task<RestObjectResponse<T>> GetJsonAsync<T>( this RestRequest request, string url, CancellationToken cancellationToken = default )
        {
            var response = await request.GetAsync( url, cancellationToken );

            return RestObjectResponse<T>.Create( response );
        }

        /// <summary>
        /// Sends a POST request to the pre-configured resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PostJsonAsync<T>( this RestRequest request, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PostAsync( content, cancellationToken );
        }

        /// <summary>
        /// Sends a POST request to the specified resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PostJsonAsync<T>( this RestRequest request, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PostAsync( url, content, cancellationToken );
        }

        /// <summary>
        /// Sends a PUT request to the pre-configured resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PutJsonAsync<T>( this RestRequest request, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PutAsync( content, cancellationToken );
        }

        /// <summary>
        /// Sends a PUT request to the specified resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PutJsonAsync<T>( this RestRequest request, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PutAsync( url, content, cancellationToken );
        }

        /// <summary>
        /// Sends a PATCH request to the pre-configured resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PatchJsonAsync<T>( this RestRequest request, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PatchAsync( content, cancellationToken );
        }

        /// <summary>
        /// Sends a PATCH request to the specified resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="url">The resource location</param>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PatchJsonAsync<T>( this RestRequest request, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PatchAsync( url, content, cancellationToken );
        }
    }
}
