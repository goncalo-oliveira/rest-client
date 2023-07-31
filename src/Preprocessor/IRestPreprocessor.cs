using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient;

/// <summary>
/// Represents a preprocessor that can be used to modify the request before it is sent
/// </summary>
public interface IRestPreprocessor
{
    /// <summary>
    /// Executes the preprocessor
    /// </summary>
    Task ExecuteAsync( HttpRequestMessage request, CancellationToken cancellationToken = default );
}
