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

The client instance provides the following HTTP methods

- GET
- POST
- PUT
- PATCH
- DELETE

All request operations respond with a `RestResponse`, containing the status code of the operation and the content, if any.

```csharp
var response = await restClient.GetAsync( "todos/1" );

if ( response.StatusCode == 200 )
{
    // do something with the content
}
```

## Scoping

It is possible to customize an operation by creating a scoped request before execution. We do that by invoking `Configure` with the path to the resource. This allows us to configure not only the headers, but also other things, such as query parameters. Here's an example.

```csharp
var response = await restClient.Configure( "users", options =>
{
    options.QueryParameters.Add( "address.city", "Bartholomebury" );
})
.GetAsync();
```

## JSON Serialization

The client includes extensions to serialize and deserialize JSON content.

```csharp
var response = await restClient.GetAsync( "todos/1" );

var todo = response.Deserialize<Todo>();
```

Or requesting with the JSON extension

```csharp
var response = await restClient.GetAsync<Todo>( "todos/1" );

var todo = response.Content;
```
