using System;

namespace Faactory.RestClient
{
    public interface IRestClientFactory
    {
        RestClient CreateClient();
        RestClient CreateClient( string name );
    }
}
