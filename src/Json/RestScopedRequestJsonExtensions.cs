using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Faactory.RestClient.Json;

namespace Faactory.RestClient
{
    public static class RestScopedRequestJsonExtensions
    {
        public static async Task<RestObjectResponse<T>> GetJsonAsync<T>( this RestScopedRequest request, CancellationToken cancellationToken = default )
        {
            var response = await request.GetAsync( cancellationToken );

            return RestObjectResponse<T>.Create( response );
        }

        public static Task<RestResponse> PostJsonAsync<T>( this RestScopedRequest request, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PostAsync( content, cancellationToken );
        }

        public static Task<RestResponse> PutJsonAsync<T>( this RestScopedRequest request, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PutAsync( content, cancellationToken );
        }

        public static Task<RestResponse> PatchJsonAsync<T>( this RestScopedRequest request, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PatchAsync( content, cancellationToken );
        }
    }
}
