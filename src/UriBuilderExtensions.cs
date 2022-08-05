using System;

namespace Faactory.RestClient;

internal static class UriBuilderExtensions
{
    /// <summary>
    /// Ensures the Uri path ends with a slash '/'
    /// </summary>
    public static UriBuilder EnsurePathTrailingSlash( this UriBuilder builder )
    {
        if ( !builder.Path.EndsWith( '/' ) )
        {
            builder.Path = string.Concat( builder.Path, '/' );
        }

        return ( builder );
    }
}
