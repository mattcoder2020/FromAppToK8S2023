using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProductService.Commands;
using ProductService.Events;
using ProductService.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductService.IntegrationTest
{
    public class BasketControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;
        public BasketControllerTest(WebApplicationFactory<Startup> factory)
        {
            // create a webapi app in memory and its client, so not IP is needed
            client = factory.CreateClient();
        }
        [Theory]
        //each Inline data entry will be fed to the parameter of the test function, in this case 'endpoint'
        //Inline data can be multiple to repeat the same test for different inputs
        //[Fact] run only once
        [InlineData("api/basket")]
        public async void Post_GivenProperBasket_ShouldReturnOk(string endpoint)
        {   //ARRANGE
           
            //ACT
            var basket = new Basket { BasketId = Guid.NewGuid().ToString(), Price = 10, ProductCategoryId = 4, Name = "demo4" };
            var stringContent = new StringContent(JsonConvert.SerializeObject(basket), Encoding.UTF8, "application/json");
            var postresponse = await client.PostAsync(endpoint, stringContent);  // post the json commnad to service so it will 
            var queryresponse = await client.GetAsync(endpoint + "/" + basket.BasketId);

            //ASSERT
            postresponse.StatusCode.Should().Be(200);
            queryresponse.StatusCode.Should().Be(200);                            // received sucess returncode 
            //queryresponse..Id.Should().Be(command.Id);                                  // Verify the rabbitmq
            //createdEvent.Name.Should().Be(command.Name);// received sucess returncode 

            //Teardown
            await client.DeleteAsync(endpoint + "/" + basket.BasketId.ToString());
        }

        [Theory]
        //each Inline data entry will be fed to the parameter of the test function, in this case 'endpoint'
        //Inline data can be multiple to repeat the same test for different inputs
        //[Fact] run only once
        [InlineData("api/basket/")]
        public async void Get_GivenNonExistId_ShouldReturn400Error(string endpoint)
        {   //ARRANGE
            var id = 100;
            await client.DeleteAsync(endpoint + "/" + id);
            //ACT
            var response = await client.DeleteAsync(endpoint + "/" + id);
            //ASSERT
            response.StatusCode.Should().Be(200);
        }
    }
}
