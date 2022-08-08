using System;
using Microsoft.Extensions.DependencyInjection;

namespace Faactory.RestClient;

/// <summary>
/// An interface to configure RestClient services
/// </summary>
public interface IRestClientBuilder
{
    IServiceCollection Services { get; }
}
