using System;

namespace Faactory.RestClient.Json
{
    internal class JsonSerializer : IJsonSerializer
    {
        private static readonly System.Text.Json.JsonSerializerOptions serializerOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        public JsonContent SerializeObject<T>( T value )
        {
            var json = System.Text.Json.JsonSerializer.Serialize( value, serializerOptions );

            return new JsonContent( json );
        }
        
        public T DeserializeObject<T>( byte[] content )
            => System.Text.Json.JsonSerializer.Deserialize<T>( content, serializerOptions );
    }
}
