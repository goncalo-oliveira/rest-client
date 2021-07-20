using  System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Faactory.RestClient
{
    public sealed class JsonContent : StringContent
    {
        internal const string JsonMediaType = "application/json";

        public JsonContent( string jsonContent )
        : base( jsonContent, Encoding.UTF8, JsonMediaType )
        {}
    }
}
