using  System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Faactory.RestClient.Json
{
    /// <summary>
    /// Provides HTTP content as JSON
    /// </summary>
    public sealed class JsonContent : ByteArrayContent
    {
        internal const string JsonMediaType = "application/json";

        public JsonContent( byte[] content )
        : base( content )
        {
            // Initialize the 'Content-Type' header with information provided by parameters.
            var headerValue = new MediaTypeHeaderValue( JsonMediaType );
            headerValue.CharSet = Encoding.UTF8.WebName;

            Headers.ContentType = headerValue;            
        }
    }
}
