// using System;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Threading;
// using System.Threading.Tasks;
// using Faactory.RestClient.Json;

// namespace Faactory.RestClient
// {
//     public static class RestRequestJsonExtensions
//     {
//         private static readonly MediaTypeWithQualityHeaderValue jsonMediaType = new MediaTypeWithQualityHeaderValue( JsonContent.JsonMediaType );

//         /// <summary>
//         /// Sends a GET request to the pre-configured resource location and deserializes from the JSON in the response body
//         /// </summary>
//         /// <param name="cancellationToken">The cancellation token to cancel operation</param>
//         /// <typeparam name="T">The type of the object to deserialize to</typeparam>
//         /// <returns>The response content if succeeded; default( T ) otherwise.</returns>
//         public static async Task<T> GetJsonAsync<T>( this IRestRequest request, string url, CancellationToken cancellationToken = default )
//         {
//             request.Configure( options =>
//             {
//                 options.AddAcceptHeader( jsonMediaType );
//             } );

//             var response = await request.GetAsync( url, cancellationToken );

//             if ( response.StatusCode == 200 )
//             {
//                 return response.Serializer.Deserialize<T>( response.Content );
//             }

//             return default( T );
//         }

//         /// <summary>
//         /// Sends a POST request to the pre-configured resource location and serializes the given value as JSON into the request body
//         /// </summary>
//         /// <param name="value">The object to serialize as JSON</param>
//         /// <param name="cancellationToken">The cancellation token to cancel operation</param>
//         /// <typeparam name="T">The type of the object to serialize</typeparam>
//         public static Task<RestResponse> PostJsonAsync<T>( this IRestRequest request, string url, T value, CancellationToken cancellationToken = default )
//         {
//             request.Configure( options =>
//             {
//                 options.AddAcceptHeader( jsonMediaType );
//             } );

//             var content = request.Client.Serializer.Serialize( value );

//             return request.PostAsync( url, new JsonContent( content ), cancellationToken );
//         }

//         /// <summary>
//         /// Sends a PUT request to the pre-configured resource location and serializes the given value as JSON into the request body
//         /// </summary>
//         /// <param name="value">The object to serialize as JSON</param>
//         /// <param name="cancellationToken">The cancellation token to cancel operation</param>
//         /// <typeparam name="T">The type of the object to serialize</typeparam>
//         public static Task<RestResponse> PutJsonAsync<T>( this IRestRequest request, string url, T value, CancellationToken cancellationToken = default )
//         {
//             request.Configure( options =>
//             {
//                 options.AddAcceptHeader( jsonMediaType );
//             } );

//             var content = request.Client.Serializer.Serialize( value );

//             return request.PutAsync( url, new JsonContent( content ), cancellationToken );
//         }

//         /// <summary>
//         /// Sends a PATCH request to the pre-configured resource location and serializes the given value as JSON into the request body
//         /// </summary>
//         /// <param name="value">The object to serialize as JSON</param>
//         /// <param name="cancellationToken">The cancellation token to cancel operation</param>
//         /// <typeparam name="T">The type of the object to serialize</typeparam>
//         public static Task<RestResponse> PatchJsonAsync<T>( this IRestRequest request, string url, T value, CancellationToken cancellationToken = default )
//         {
//             request.Configure( options =>
//             {
//                 options.AddAcceptHeader( jsonMediaType );
//             } );

//             var content = request.Client.Serializer.Serialize( value );

//             return request.PatchAsync( url, new JsonContent( content ), cancellationToken );
//         }
//     }
// }
