// using System;
// using System.Text;
// using System.Text.Json;

// namespace Faactory.RestClient
// {
//     /// <summary>
//     /// Experimental extensions for REST-SCHEMA
//     /// </summary>
//     public static class RestRequestOptionsSchemaExtensions
//     {
//         private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
//         {
//             PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//         };

//         private const string schemaMap = "X-Schema-Map";
//         private const string schemaInclude = "X-Schema-Include";


//         /// <summary>
//         /// Add X-Schema-Map header with the given schema data
//         /// <br/>
//         /// THIS IS EXPERIMENTAL CODE. IT MIGHT CHANGE, DISAPPEAR OR BREAK THINGS.
//         /// </summary>
//         /// <param name="schema">The schema data</param>
//         public static RestRequestOptions SchemaMap( this RestRequestOptions options, object schema )
//         {
//             AddSchemaHeader( options, schemaMap, schema );

//             return ( options );
//         }

//         /// <summary>
//         /// Add X-Schema-Include header with the given schema data
//         /// <br/>
//         /// THIS IS EXPERIMENTAL CODE. IT MIGHT CHANGE, DISAPPEAR OR BREAK THINGS.
//         /// </summary>
//         /// <param name="schema">The schema data</param>
//         public static RestRequestOptions SchemaInclude( this RestRequestOptions options, object schema )
//         {
//             AddSchemaHeader( options, schemaInclude, schema );

//             return ( options );
//         }

//         private static void AddSchemaHeader( RestRequestOptions options, string headerName, object schema )
//         {
//             var json = JsonSerializer.Serialize( schema, jsonSerializerOptions );
//             var value = Convert.ToBase64String( Encoding.UTF8.GetBytes( json ) );

//             // remove any previous X-Schema-Map or X-Schema-Include headers
//             if ( options.Headers.Contains( schemaMap ) )
//             {
//                 options.Headers.Remove( schemaMap );
//             }

//             if ( options.Headers.Contains( schemaInclude ) )
//             {
//                 options.Headers.Remove( schemaInclude );
//             }

//             // add schema header
//             options.Headers.Add( headerName, value );
//         }
//     }
// }
