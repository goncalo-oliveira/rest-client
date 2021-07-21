using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Faactory.RestClient
{
    /// <summary>
    /// Configurable options for a scoped request
    /// </summary>
    public sealed class RestRequestOptions
    {
        internal RestRequestOptions()
        {
            Headers = new HttpRequestMessage().Headers;
            QueryParameters = new NameValueCollection();
        }

        /// <summary>
        /// Gets the request headers
        /// </summary>
        public HttpRequestHeaders Headers { get; }

        /// <summary>
        /// Gets the request query parameters
        /// </summary>
        public NameValueCollection QueryParameters { get; internal set; }
    }
}
