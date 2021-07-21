# REST Client for .NET

This projects contains the implementation for a REST Client for .NET. It's built on top of the original `HttpClient` and fully supports `IHttpClientFactory` for dependency injection.

## Getting started

Install the package.

```shell
$ todo
```

To create a client instance using dependency injection

```csharp
IServiceCollection services = new ServiceCollection()
    ...
    .AddRestClient( "jsonplaceholder", "https://jsonplaceholder.typicode.com" )
    ...

var provider = services.BuildServiceProvider();

var restClient = provider.GetService<IRestClientFactory>()
    .CreateClient( "jsonplaceholder" );

```

If creating the client manually, an `HttpClient` instance needs to be passed through

```csharp
var httpClient = ...
var restClient = new RestClient( httpClient, "https://jsonplaceholder.typicode.com" );
```

## Using

All request operations respond with a `RestResponse` or `RestObjectResponse`, containing the status code of the operation and the content, if any.

```csharp
var response = await restClient.GetAsync( "todos/1" );

if ( response.IsOk() )
{
    // do something with the content only if response is a 200 OK
}
```

## Customization and Scoping

It is possible to customize and/or scope an operation by creating a request before execution. We do that by invoking `Configure` with or without the path to the resource. This allows us to configure not only the headers, but also other things, such as query parameters. This method returns a `RestRequest` instance (this instance is reusable).

>Note: If we create a scoped request and then invoke the operation with a url, the latter overrides the scoped url.

Here's an example of a scoped request

```csharp
var response = await restClient.Configure( "users", options =>
{
    options.QueryParameters.Add( "address.city", "Bartholomebury" );
})
.GetAsync();
```

And another one of a non-scoped request

```csharp
var response = await restClient.Configure( options =>
{
    options.Headers.Add( "X-Custom-Header", "custom header value" );
})
.GetAsync( "users" );
```

Both scoped and non-scoped request instances are reusable and multiple operations can be performed with the same request instance.
Here's an example on reusing a non-scoped request.

```csharp
var request = restClient.Configure( options =>
{
    options.Headers.Add( "X-Custom-Header", "custom header value" );
} );

var ids = new int[] { 1, 2, 3 };

var getTasks = ids.Select( id => request.GetAsync( $"users/{id}" ) )
    .ToArray();

var responses = await Task.WhenAll( getTasks );
```

## Working with JSON

The client includes extensions to serialize and deserialize JSON content. This can be done by manually deserializing a `RestResponse` instance

```csharp
var response = await restClient.GetAsync( "todos/1" );

var todo = response.Deserialize<Todo>();
```

Or requesting directly with the JSON extension, which returns a `RestObjectResponse` instead

```csharp
var response = await restClient.GetJsonAsync<Todo>( "todos/1" );

var todo = response.Content;
```

In both scenarios you will have access to the response status code and headers.

## Polymorphic JSON Serialization

The client by default uses Microsoft's JSON serializer, therefore, there is limited support for polymorphic serialization and deserialization is not supported at all. You can find more in [this article](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism) where you will also find a few workarounds, including writing a custom converter.

If you rather use [Newtonsoft's Json.NET](https://www.newtonsoft.com/json) (or any other), you can easilly write a custom serializer. Here's an example for Newtonsoft's

```csharp
public class NewtonsoftJsonSerializer : IJsonSerializer
{
    public JsonContent SerializeObject<T>( T value )
    {
        var json = JsonConvert.SerializeObject( value );

        return new JsonContent( json );
    }
    
    public T DeserializeObject<T>( byte[] content )
        => JsonConvert.DeserializeObject<T>( content );
}
```

If you are using dependency injection, you can add the serializer by injecting it into the container services.

```csharp
IServiceCollection services = new ServiceCollection()
...
services.AddRestClient( "jsonplaceholder", "https://jsonplaceholder.typicode.com" )

// add our custom serializer
services.AddSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
```

If you are not using dependency injection, just pass it into the constructor.

```csharp
var httpClient = ...
var restClient = new RestClient( 
    httpClient,
    "https://jsonplaceholder.typicode.com",
    new NewtonsoftJsonSerializer() );
```
