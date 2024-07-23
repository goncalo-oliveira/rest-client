using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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
            var headerValue = new MediaTypeHeaderValue( JsonMediaType )
            {
                CharSet = Encoding.UTF8.WebName
            };

            Headers.ContentType = headerValue;            
        }
    }
}
