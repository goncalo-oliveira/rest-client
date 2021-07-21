using System;

namespace Faactory.RestClient.Json
{
    /// <summary>
    /// An interface to the JSON serialization
    /// </summary>
    public interface IJsonSerializer
    {
        JsonContent SerializeObject<T>( T value );
        T DeserializeObject<T>( byte[] content );
    }
}
