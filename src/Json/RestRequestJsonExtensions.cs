using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    public static class RestRequestJsonExtensions
    {
        public static async Task<RestObjectResponse<T>> GetJsonAsync<T>( this RestRequest request, CancellationToken cancellationToken = default )
        {
            var response = await request.GetAsync( cancellationToken );

            return RestClientJsonExtensions.CreateObjectResponse<T>( response );
        }

        public static async Task<RestObjectResponse<T>> PostJsonAsync<T>( this RestRequest request, T value, CancellationToken cancellationToken = default )
        {
            var content = RestClientJsonExtensions.CreateJsonContent( value );
            var response = await request.PostAsync( content, cancellationToken );

            return RestClientJsonExtensions.CreateObjectResponse<T>( response );
        }

        public static async Task<RestObjectResponse<T>> PutJsonAsync<T>( this RestRequest request, T value, CancellationToken cancellationToken = default )
        {
            var content = RestClientJsonExtensions.CreateJsonContent( value );
            var response = await request.PutAsync( content, cancellationToken );

            return RestClientJsonExtensions.CreateObjectResponse<T>( response );
        }

        public static async Task<RestObjectResponse<T>> PatchJsonAsync<T>( this RestRequest request, T value, CancellationToken cancellationToken = default )
        {
            var content = RestClientJsonExtensions.CreateJsonContent( value );
            var response = await request.PatchAsync( content, cancellationToken );

            return RestClientJsonExtensions.CreateObjectResponse<T>( response );
        }
    }
}
