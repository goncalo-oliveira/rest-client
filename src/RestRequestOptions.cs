using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Faactory.RestClient
{
    public sealed class RestRequestOptions
    {
        internal RestRequestOptions()
        {
            Headers = new HttpRequestMessage().Headers;
            QueryParameters = new NameValueCollection();
        }

        public HttpRequestHeaders Headers { get; }
        public NameValueCollection QueryParameters { get; internal set; }
    }
}
