using System;
using System.Net.Http.Headers;

namespace Faactory.RestClient
{
    internal static class HttpHeadersExtensions
    {
        /// <summary>
        /// Copies HTTP headers from another collection
        /// </summary>
        public static void CopyFrom( this HttpHeaders source, HttpHeaders headers )
        {
            foreach ( var header in headers )
            {
                source.Add( header.Key, header.Value );
            }
        }
    }
}
