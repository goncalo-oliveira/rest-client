using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient;

/// <summary>
/// A raw rest response
/// </summary>
public sealed class RestResponse : IRestResponse
{
    internal static readonly RestResponse Empty = new( new HttpResponseMessage(), new Json.DefaultJsonSerializer() );

    internal RestResponse( HttpResponseMessage httpResponse, ISerializer serializer )
    {
        Headers = httpResponse.Headers;
        StatusCode = (int)httpResponse.StatusCode;
        ContentType = httpResponse.Content?.Headers.ContentType?.MediaType;
        Version = httpResponse.Version;
        Serializer = serializer;
    }

    /// <summary>
    /// Gets the response headers
    /// </summary>
    public System.Net.Http.Headers.HttpResponseHeaders Headers { get; }

    /// <summary>
    /// Gets the response status code
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Gets the response content type
    /// </summary>
    public string? ContentType { get; }

    /// <summary>
    /// Gets the response content as a byte array
    /// </summary>
    public byte[] Content { get; internal set; } = Array.Empty<byte>();

    /// <summary>
    /// Gets the duration of the request
    /// </summary>
    public TimeSpan Duration { get; internal set; }

    /// <summary>
    /// Gets the HTTP version
    /// </summary>
    public Version Version { get; }

    internal ISerializer Serializer { get; }
}
