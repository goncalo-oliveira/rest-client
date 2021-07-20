using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    public sealed class RestObjectResponse<T> : IRestResponse<T>, IRestResponse
    {
        internal RestObjectResponse()
        {}

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; internal set; }
        public int StatusCode { get; internal set; }
        public T Content { get; internal set; }
    }
}
