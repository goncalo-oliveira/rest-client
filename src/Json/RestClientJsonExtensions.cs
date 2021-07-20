using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    public static class RestClientJsonExtensions
    {
        internal static System.Text.Json.JsonSerializerOptions serializerOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        public static async Task<RestObjectResponse<T>> GetJsonAsync<T>( this RestClient client, string url, CancellationToken cancellationToken = default )
        {
            var response = await client.GetAsync( url, cancellationToken );

            return CreateObjectResponse<T>( response );
        }

        public static async Task<RestObjectResponse<T>> PostJsonAsync<T>( this RestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = CreateJsonContent( value );
            var response = await client.PostAsync( url, content, cancellationToken );

            return CreateObjectResponse<T>( response );
        }

        public static async Task<RestObjectResponse<T>> PutJsonAsync<T>( this RestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = CreateJsonContent( value );
            var response = await client.PutAsync( url, content, cancellationToken );

            return CreateObjectResponse<T>( response );
        }

        public static async Task<RestObjectResponse<T>> PatchJsonAsync<T>( this RestClient client, string url, T value, CancellationToken cancellationToken = default )
        {
            var content = CreateJsonContent( value );
            var response = await client.PatchAsync( url, content, cancellationToken );

            return CreateObjectResponse<T>( response );
        }

        internal static JsonContent CreateJsonContent<T>( T value )
        {
            // TODO: add support for Newtonsoft
            var json = System.Text.Json.JsonSerializer.Serialize( value, serializerOptions );

            return new JsonContent( json );
        }

        internal static RestObjectResponse<T> CreateObjectResponse<T>( RestResponse response )
        {
            var restResponse = new RestObjectResponse<T>
            {
                StatusCode = response.StatusCode,
                Headers = response.Headers
            };

            if ( restResponse.StatusCode == 200 )
            {
                try
                {
                    // TODO: add support for Newtonsoft
                    restResponse.Content = System.Text.Json.JsonSerializer.Deserialize<T>( response.Content, serializerOptions );
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
