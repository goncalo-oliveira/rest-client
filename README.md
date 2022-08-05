# REST Client for .NET

This projects contains the implementation for a REST Client for .NET. It's built on top of the original `HttpClient` and fully supports `IHttpClientFactory` for dependency injection.

## Getting started

Install the package.

```shell
$ dotnet add package Faactory.RestClient
```

To create a client instance using dependency injection

```csharp
IServiceCollection services = new ServiceCollection()
    ...
    .AddRestClient( "jsonplaceholder", "https://jsonplaceholder.typicode.com" )
    ...
```

This will give you access to an `IRestClientFactory` interface. Then, wherever you need a client

```csharp
public class MyClass
{
    private readonly IRestClient client;

    public MyClass( IRestClientFactory clientFactory )
    {
        client = clientFactory.CreateClient( "jsonplaceholder" );
    }
    ...
}
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

## Configuring and Scoping

It is possible to customize a request's configuration, such as headers or query parameters. We do that by invoking `Configure` and a configuration method, which returns a new instance.

```csharp
IRestClient configured = restClient.Configure( options =>
{
    options.QueryParameters.Add( "address.city", "Bartholomebury" );
} );

var response = configured.GetAsync( "users" );

// or directly with a fluent syntax

var response = await restClient.Configure( options =>
{
    options.QueryParameters.Add( "address.city", "Bartholomebury" );
})
.GetAsync( "users" );
```

It is also possible to scope the request with the path to the resource while configuring a request. To do that, we invoke the `Configure` with the path and the configuration method. This will return an `IRestRequest` instead of an `IRestClient`.

```csharp
IRestRequest scoped = restClient.Configure( "users", options =>
{
    options.QueryParameters.Add( "address.city", "Bartholomebury" );
} );

var response = scoped.GetAsync();

// or directly with a fluent syntax

var response = await restClient.Configure( "users", options =>
{
    options.QueryParameters.Add( "address.city", "Bartholomebury" );
})
.GetAsync();
```

> You will notice that `IRestRequest` operation methods don't take a path, since it was already defined with the configuration method.

It's worth noting that both `IRestClient` and scoped `IRestRequest` instances are reusable and multiple operations can be performed with the same instance. Here's an example on reusing a non-scoped request.

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

Or directly with the request extension, which return the deserialized instance instead; be aware that this method will only deserialize the content if the status code is a 100 (Ok), otherwise it will return `default<T>`. Something else to consider is that this method doesn't give us access to the response's status code and headers.

```csharp
var todo = await restClient.GetJsonAsync<Todo>( "todos/1" );
```

For the other request operations, the serialization process is applied the other way around and the response is always a `RestResponse`. For example, a POST request

```csharp
Todo todo = ...;

var response = await restClient.PostJsonAsync( "todos", todo );
```

## Polymorphic JSON Serialization

By default, the client uses Microsoft's JSON serializer, therefore, there is limited support for polymorphic serialization and deserialization is not supported at all. You can find more information in [this article](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism) where you will also find a few workarounds, including writing a custom converter.

If you rather use [Newtonsoft's Json.NET](https://www.newtonsoft.com/json) (or any other), you can easilly write a custom serializer. Here's an example for Newtonsoft's

```csharp
public class NewtonsoftJsonSerializer : ISerializer
{
    public byte[] SerializeObject<T>( T value )
    {
        var json = JsonConvert.SerializeObject( value );

        return Encoding.UTF8.GetBytes( json );
    }
    
    public T DeserializeObject<T>( byte[] content )
    {
        var json = Encoding.UTF8.GetString( content );

        return JsonConvert.DeserializeObject<T>( json );
    }
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

You can also pass a custom serializer if deserializing from a `RestResponse` instance

```csharp
var customSerializer = new NewtonsoftJsonSerializer();

var response = await restClient.GetAsync( "todos/1" );

var todo = response.Deserialize<Todo>( customSerializer );
```

## Authorization header

You can use extension methods to configure the `Authorization` header. Currently, the supported schemes are

- Basic authentication
- Bearer token

This can be applied to the entire client, when configuring the underlying `HttpClient` instance with dependency injection

```csharp
IServiceCollection services = new ServiceCollection()
    ...
    .AddRestClient( "jsonplaceholder", "https://jsonplaceholder.typicode.com", httpClient =>
    {
        httpClient.AddBasicAuthentication( "username", "password" );
    } )
    ...
```

when manually creating the client instance, by accessing the underlying client extensions

```csharp
var httpClient = ...

httpClient.AddBasicAuthentication( "username", "password" );

var restClient = new RestClient( httpClient, "https://jsonplaceholder.typicode.com" );

// accessing the underlying client instance works the same
// restClient.HttpClient.AddBasicAuthentication( "username", "password" );

```

or when customizing/scoping a request

```csharp
var response = await restClient.Configure( "users", options =>
{
    options.AddBasicAuthentication( "username", "password" );
})
.GetAsync();
```

> Note: These extensions require adding the namespace `Faactory.RestClient`

## EXPERIMENTAL: REST-Schema Extensions

If you are working with an API that is compatible with [REST-Schema](https://github.com/goncalo-oliveira/rest-schema-spec), there are a few extensions that you can use. These are experimental features, so they might change in the future, disappear or not function properly.

We can send a map schema spec through the headers by customizing a request with the available extensions.

```csharp
var response = await restClient.Configure( "users", options =>
{
    options.SchemaMap( new {
        spec = new {
            _ = new string[] { "id", "name", "email", "address" },
            address = new string[] { "street", "city", "zipcode" }
        }
    } );
})
.GetAsync();
```

Similarly we can send an include schema spec.

```csharp
var response = await restClient.Configure( "users", options =>
{
    options.SchemaInclude( new {
        spec = new {
            _ = new string[] { "address" }
        }
    } );
})
.GetAsync();
```
