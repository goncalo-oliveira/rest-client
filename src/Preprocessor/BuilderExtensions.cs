using Faactory.RestClient;

namespace Microsoft.Extensions.DependencyInjection;

public static class RestClientBuilderPreprocessorExtensions
{
    /// <summary>
    /// Registers a preprocessor. Preprocessors are executed in the order they are added.
    /// </summary>
    public static IRestClientBuilder AddPreprocessor<TPreprocessor>( this IRestClientBuilder builder )
        where TPreprocessor : class, IRestPreprocessor
    {
        builder.Services.AddTransient<IRestPreprocessor, TPreprocessor>();

        return ( builder );
    }
}
