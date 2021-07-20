using System;

namespace Faactory.RestClient
{
    public static class RestResponseJsonExtensions
    {
        public static T Deserialize<T>( this RestResponse response )
        {
            var objResponse = RestClientJsonExtensions.CreateObjectResponse<T>( response );

            return ( objResponse.Content );
        }
    }
}
