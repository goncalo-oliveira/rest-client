using System;
using Microsoft.Extensions.DependencyInjection;

namespace Faactory.RestClient;

/// <summary>
/// An interface to configure RestClient services
/// </summary>
public interface IRestClientBuilder : IHttpClientBuilder
{
    /*
    This interface is just a wrapper around IHttpClientBuilder.

    In future versions, it may become deprecated and removed.
    For now, it's still used to extend functionality.
    */
}
