using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
            request.AddAcceptHeader();

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
            request.AddAcceptHeader();

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
            request.AddAcceptHeader();

            var content = request.Client.Serializer.Serialize( value );

            return request.PostAsync( new JsonContent( content ), cancellationToken );
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
            request.AddAcceptHeader();

            var content = request.Client.Serializer.Serialize( value );

            return request.PostAsync( url, new JsonContent( content ), cancellationToken );
        }

        /// <summary>
        /// Sends a PUT request to the pre-configured resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PutJsonAsync<T>( this RestRequest request, T value, CancellationToken cancellationToken = default )
        {
            request.AddAcceptHeader();

            var content = request.Client.Serializer.Serialize( value );

            return request.PutAsync( new JsonContent( content ), cancellationToken );
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
            request.AddAcceptHeader();

            var content = request.Client.Serializer.Serialize( value );

            return request.PutAsync( url, new JsonContent( content ), cancellationToken );
        }

        /// <summary>
        /// Sends a PATCH request to the pre-configured resource location and serializes the given value as JSON into the request body
        /// </summary>
        /// <param name="value">The object to serialize as JSON</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        public static Task<RestResponse> PatchJsonAsync<T>( this RestRequest request, T value, CancellationToken cancellationToken = default )
        {
            request.AddAcceptHeader();

            var content = request.Client.Serializer.Serialize( value );

            return request.PatchAsync( new JsonContent( content ), cancellationToken );
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
            request.AddAcceptHeader();

            var content = request.Client.Serializer.Serialize( value );

            return request.PatchAsync( url, new JsonContent( content ), cancellationToken );
        }

        /// <summary>
        /// Adds the json Accept header if not set already
        /// </summary>
        private static void AddAcceptHeader( this RestRequest request )
        {
            if ( !request.Headers.Accept.Contains( new MediaTypeWithQualityHeaderValue( JsonContent.JsonMediaType ) ) )
            {
                request.Headers.Accept.Add( new MediaTypeWithQualityHeaderValue( JsonContent.JsonMediaType ) );
            }
        }
    }
}
