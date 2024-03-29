using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient;

/// <summary>
/// An interface to a rest response
/// </summary>
public interface IRestResponse
{
    System.Net.Http.Headers.HttpResponseHeaders Headers { get; }
    int StatusCode { get; }

    TimeSpan Duration { get; }
}
