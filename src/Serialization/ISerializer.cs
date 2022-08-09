using System;

namespace Faactory.RestClient;

/// <summary>
/// An interface to object serialization
/// </summary>
public interface ISerializer
{
    byte[] Serialize<T>( T value );
    T Deserialize<T>( byte[] content );
}
