using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Faactory.RestClient.Json;

namespace Faactory.RestClient
{
    public static class RestRequestJsonExtensions
    {
        public static async Task<RestObjectResponse<T>> GetJsonAsync<T>( this RestRequest request, string url, CancellationToken cancellationToken = default )
        {
            var response = await request.GetAsync( url, cancellationToken );

            return RestObjectResponse<T>.Create( response );
        }

        public static Task<RestResponse> PostJsonAsync<T>( this RestRequest request, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PostAsync( url, content, cancellationToken );
        }

        public static Task<RestResponse> PutJsonAsync<T>( this RestRequest request, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PutAsync( url, content, cancellationToken );
        }

        public static Task<RestResponse> PatchJsonAsync<T>( this RestRequest request, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = request.Client.JsonSerializer.SerializeObject( value );

            return request.PatchAsync( url, content, cancellationToken );
        }
    }
}
