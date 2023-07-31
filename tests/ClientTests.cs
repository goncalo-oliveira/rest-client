using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Faactory.RestClient.Tests
{
    public class ClientTests
    {
        private readonly IRestClient client;

        public ClientTests( IRestClientFactory clientFactory )
        {
            client = clientFactory.CreateClient( "jsonplaceholder" );
        }

        [Fact]
        public async Task TestGetTodoList()
        {
            var response = await client.GetAsync( "todos" );

            Assert.True( response.IsOk() );

            var todoList = response.Deserialize<IEnumerable<Todo>>();

            Assert.NotNull( todoList );
            Assert.NotEmpty( todoList );

            var todo = todoList.First();

            Assert.True( todo.Id > 0 );
            Assert.True( todo.UserId > 0 );
            Assert.False( string.IsNullOrEmpty( todo.Title ) );
        }

        [Fact]
        public async Task TestGetTodoJson()
        {
            var todo = await client.GetJsonAsync<Todo>( "todos/1" );

            Assert.NotNull( todo );

            Assert.Equal( 1, todo.Id );
            Assert.Equal( 1, todo.UserId );
            Assert.Equal( "delectus aut autem", todo.Title );
        }

        [Fact]
        public async Task TestGetTodoDeserialize()
        {
            var response = await client.GetAsync( "todos/1" );

            Assert.Equal( 200, response.StatusCode );
            Assert.NotNull( response.Content );

            var todo = response.Deserialize<Todo>();

            Assert.NotNull( todo );

            Assert.Equal( 1, todo.Id );
            Assert.Equal( 1, todo.UserId );
            Assert.Equal( "delectus aut autem", todo.Title );
        }

        [Fact]
        public async Task TestDeleteTodo()
        {
            var response = await client.DeleteAsync( "todos/1" );

            Assert.True( response.IsOk() );
        }

        [Fact]
        public async Task TestPostTodo()
        {
            var todo = new Todo
            {
                UserId = 1,
                Title = "item",
                Completed = true
            };

            var response = await client.PostJsonAsync( "todos", todo );

            Assert.True( response.IsCreated() );

            var item = response.Deserialize<Todo>();

            Assert.NotNull( item );

            Assert.Equal( todo.UserId, item.UserId );
            Assert.Equal( todo.Title, item.Title );
            Assert.Equal( todo.Completed, item.Completed );
        }
 
        [Fact]
        public async Task TestHttpVersion()
        {
            var request = client.Configure( options =>
            {
                Assert.Equal( client.HttpClient.DefaultRequestVersion, options.Version );
                Assert.Equal( client.HttpClient.DefaultVersionPolicy, options.VersionPolicy );

                options.Version = System.Net.HttpVersion.Version20;
                options.VersionPolicy = System.Net.Http.HttpVersionPolicy.RequestVersionExact;
            } );

            request.Configure( options =>
            {
                Assert.Equal( System.Net.HttpVersion.Version20, options.Version );
                Assert.Equal( System.Net.Http.HttpVersionPolicy.RequestVersionExact, options.VersionPolicy );
            } );

            var response = await request.GetAsync( "todos/1" );

            Assert.Equal( 200, response.StatusCode );
            Assert.Equal( System.Net.HttpVersion.Version20, response.Version );
        }
    }
}
