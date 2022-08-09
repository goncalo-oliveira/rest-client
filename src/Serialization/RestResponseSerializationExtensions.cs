namespace Faactory.RestClient;

/// <summary>
/// Extensions for the rest responses
/// </summary>
public static class RestResponseSerializationExtensions
{
    /// <summary>
    /// Deserializes the response content into an object using the default serializer
    /// </summary>
    /// <param name="response">The response to deserialize the content from</param>
    /// <typeparam name="T">The type of the object to deserialize to</typeparam>
    /// <returns>A deserialized object instance</returns>
    public static T Deserialize<T>( this RestResponse response )
        => Deserialize<T>( response, response.Serializer );

    /// <summary>
    /// Deserializes the response content into an object using the given serializer
    /// </summary>
    /// <param name="response">The response to deserialize the content from</param>
    /// <param name="serializer">The content serializer to use for deserialization</param>
    /// <typeparam name="T">The type of the object to deserialize to</typeparam>
    /// <returns>A deserialized object instance</returns>
    public static T Deserialize<T>( this RestResponse response, ISerializer serializer )
    {
        if ( ( response.Content == null ) || ( response.Content.Length == 0 ) )
        {
            // no content
            return default( T );
        }

        try
        {
            return serializer.Deserialize<T>( response.Content );
        }
        catch
        {
            // we are ignoring deserialization errors and returning null
            return default( T );
        }
    }
}
