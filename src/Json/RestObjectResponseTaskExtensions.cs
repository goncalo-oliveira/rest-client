using System;
using System.Threading.Tasks;
using Faactory.RestClient.Json;

namespace Faactory.RestClient;

public static class RestObjectResponseTaskExtensions
{
    /// <summary>
    /// Retrieves the response content if the status code is 200 (OK)
    /// </summary>
    /// <returns>The response content if succeeded; default( T ) otherwise.</returns>
    public static async Task<T> GetContentAsync<T>( this Task<RestObjectResponse<T>> objectResponseTask )
    {
        var result = await objectResponseTask.ConfigureAwait( false );

        if ( result.IsOk() )
        {
            return ( result.Content );
        }

        return default;
    }
}
