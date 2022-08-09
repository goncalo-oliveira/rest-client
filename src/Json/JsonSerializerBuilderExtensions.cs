using System;
using System.Text.Json;
using Faactory.RestClient;

namespace Microsoft.Extensions.DependencyInjection;

public static class JsonSerializerRestClientBuilderExtensions
{
    /// <summary>
    /// Registers an action used to configure JsonSerializerOptions
    /// </summary>
    public static IRestClientBuilder ConfigureJsonSerializer( this IRestClientBuilder builder, Action<JsonSerializerOptions> configure )
    {
        builder.Services.Configure<JsonSerializerOptions>( configure );

        return ( builder );
    }
}
