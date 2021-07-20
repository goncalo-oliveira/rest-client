using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    public interface IRestResponse
    {
        System.Net.Http.Headers.HttpResponseHeaders Headers { get; }
        int StatusCode { get; }
    }

    public interface IRestResponse<T> : IRestResponse
    {
        T Content { get; }
    }
}
