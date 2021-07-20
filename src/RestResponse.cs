using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    public sealed class RestResponse : IRestResponse<byte[]>, IRestResponse
    {
        internal static readonly RestResponse Empty = new RestResponse();

        internal RestResponse()
        {}

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; internal set; }
        public int StatusCode { get; internal set; }
        public byte[] Content { get; internal set; }
    }
}
