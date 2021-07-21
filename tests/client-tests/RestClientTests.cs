using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faactory.RestClient;
using Faactory.RestClient.Json;
using Xunit;

namespace Faactory.RestClient.Tests
{
    public class ClientTests
    {
        private readonly RestClient client;

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
            var response = await client.GetJsonAsync<Todo>( "todos/1" );

            Assert.True( response.IsOk() );
            Assert.NotNull( response.Content );

            Assert.Equal( 1, response.Content.Id );
            Assert.Equal( 1, response.Content.UserId );
            Assert.Equal( "delectus aut autem", response.Content.Title );
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
    }
}
