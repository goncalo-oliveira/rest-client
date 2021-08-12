using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    /// <summary>
    /// An interface to a rest response
    /// </summary>
    public interface IRestResponse
    {
        System.Net.Http.Headers.HttpResponseHeaders Headers { get; }
        int StatusCode { get; }
    }

    /// <summary>
    /// An inteface to a typed rest response
    /// </summary>
    /// <typeparam name="T">The type of the content</typeparam>
    public interface IRestResponse<T> : IRestResponse
    {
        string ContentType { get; }
        T Content { get; }
    }
}
