using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProductService.Commands;
using ProductService.Events;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductService.IntegrationTest
{
    public class ProductServiceControllerTest : IClassFixture<WebApplicationFactory<Startup>>, IClassFixture<RabbitMqFixture>
    {
        private readonly HttpClient client;
        private readonly RabbitMqFixture fixture;
        public ProductServiceControllerTest(WebApplicationFactory<Startup> factory, RabbitMqFixture rabbitmqfixture)
        {
            client = factory.CreateClient();
            fixture = rabbitmqfixture;
        }
        [Theory]
        [InlineData("api/product")]
        public async void Post_GivenProperCommand_ShouldReturnOk(string endpoint)
        {   //ARRANGE
            string queueName = null;
            var source = await fixture.SubscribeAndGetAsync<ProductCreated>(   //subscribe to rabbitmq 
                onMessageReceived: (@event, @taskcompletion) =>
                {
                    @taskcompletion.SetResult(@event);
                    return Task.CompletedTask;
                }
                , queueName: queueName);
            //ACT
            var command = new NewProductCommand { Id = 10, Price = 10, Category = "any", Name = "demo1" };
            var stringContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endpoint, stringContent);  // post the json commnad to service so it will 
            response.StatusCode.Should().Be(200);                            // publish to rabbitmq
            
            //ASSERT
            var createdEvent = await source.Task;   
            createdEvent.Id.Should().Be(10);                                  // Verify the rabbitmq
            createdEvent.Name.Should().Be("demo1");

            //YOU CAN ADD validation to command persisted to DB 
        }
    }
}
