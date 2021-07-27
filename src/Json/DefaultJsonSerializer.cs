using System;
using System.Text;

namespace Faactory.RestClient.Json
{
    internal class DefaultJsonSerializer : ISerializer
    {
        private static readonly System.Text.Json.JsonSerializerOptions serializerOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        public byte[] Serialize<T>( T value )
        {
            var json = System.Text.Json.JsonSerializer.Serialize( value, serializerOptions );

            return Encoding.UTF8.GetBytes( json );
        }
        
        public T Deserialize<T>( byte[] content )
            => System.Text.Json.JsonSerializer.Deserialize<T>( content, serializerOptions );
    }
}
