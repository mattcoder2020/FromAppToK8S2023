using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProductService.Commands;
using ProductService.Events;
using ProductService.Models;
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
            // create a webapi app in memory and its client, so not IP is needed
            client = factory.CreateClient();
            fixture = rabbitmqfixture;
        }
        [Theory]
        //each Inline data entry will be fed to the parameter of the test function, in this case 'endpoint'
        //Inline data can be multiple to repeat the same test for different inputs
        //[Fact] run only once
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
            var command = new NewProductCommand { Id = 22, Price = 10, CategoryId = 1, Name = "demo1" };
            var stringContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endpoint, stringContent);  // post the json commnad to service so it will 
            var createdEvent = await source.Task;

            //ASSERT
            response.StatusCode.Should().Be(200);                            // received sucess returncode 
            createdEvent.Id.Should().Be(command.Id);                                  // Verify the rabbitmq
            createdEvent.Name.Should().Be("demo1");
            //response = await client.GetAsync(endpoint+"/"+ command.Id.ToString());
            //var entityReturnFromDB = await response.Content.ReadAsAsync<Product>();
            ////YOU CAN ADD validation to get api to verify if command persisted to DB 
            //entityReturnFromDB.Id.Should().Be(command.Id);
            //entityReturnFromDB.Name.Should().Be("demo1");

            //Teardown
            await client.DeleteAsync(endpoint + "/" + command.Id.ToString());
        }
    }
}
