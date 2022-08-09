using System;
using System.Text;

namespace Faactory.RestClient.Json
{
    internal class DefaultJsonSerializer : ISerializer
    {
        private readonly System.Text.Json.JsonSerializerOptions jsonSerializerOptions;

        internal static void ConfigureJsonSerializer( System.Text.Json.JsonSerializerOptions options )
        {
            options.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            options.PropertyNameCaseInsensitive = true;
        }

        public DefaultJsonSerializer()
        {
            jsonSerializerOptions = new System.Text.Json.JsonSerializerOptions();

            ConfigureJsonSerializer( jsonSerializerOptions );
        }

        public DefaultJsonSerializer( System.Text.Json.JsonSerializerOptions jsonSerializerOptions )
        {
            this.jsonSerializerOptions = jsonSerializerOptions;
        }

        public byte[] Serialize<T>( T value )
        {
            var json = System.Text.Json.JsonSerializer.Serialize( value, jsonSerializerOptions );

            return Encoding.UTF8.GetBytes( json );
        }
        
        public T Deserialize<T>( byte[] content )
            => System.Text.Json.JsonSerializer.Deserialize<T>( content, jsonSerializerOptions );
    }
}
