using System;
using Faactory.RestClient;

namespace Microsoft.Extensions.DependencyInjection;

public static class SerializerRestClientBuilderExtensions
{
    /// <summary>
    /// Adds a serializer to the specified IRestClientBuilder service collection
    /// </summary>
    public static IRestClientBuilder AddSerializer<TSerializer>( this IRestClientBuilder builder ) where TSerializer : class, ISerializer
    {
        builder.Services.AddTransient<ISerializer, TSerializer>();

        return ( builder );
    }

    /// <summary>
    /// Adds a serializer to the specified IRestClientBuilder service collection
    /// </summary>
    public static IRestClientBuilder AddSerializer( this IRestClientBuilder builder, Type serializerType )
    {
        builder.Services.AddTransient( typeof( ISerializer ), serializerType );

        return ( builder );
    }
}
