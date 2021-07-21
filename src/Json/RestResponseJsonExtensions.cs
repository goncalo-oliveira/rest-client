using System;

namespace Faactory.RestClient
{
    public static class RestResponseJsonExtensions
    {
        /// <summary>
        /// Deserializes the response content as JSON into an object
        /// </summary>
        /// <param name="response">The response to deserialize the content from</param>
        /// <typeparam name="T">The type of the object to deserialize to</typeparam>
        /// <returns>A deserialized object instance</returns>
        public static T Deserialize<T>( this RestResponse response )
        {
            if ( ( response.Content == null ) || ( response.Content.Length == 0 ) )
            {
                // no content
                return default( T );
            }

            try
            {
                return response.JsonSerializer.DeserializeObject<T>( response.Content );
            }
            catch
            {
                // we are ignoring deserialization errors and returning null
                return default( T );
            }
        }
    }
}
