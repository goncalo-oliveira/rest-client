using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient;

/// <summary>
/// An scoped client instance
/// </summary>
public interface IRestRequest : IRestClient
{
    IRestClient Client { get; }
}
