using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProductService.Events;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OrderService;
using Xunit;
using OrderService.SQLiteDB;
using Common.DataAccess;
using OrderService.Models;
using System;
using System.Collections.Generic;

namespace OrderService.IntegrationTest
{
    public class OrderServiceControllerTest : IClassFixture<WebApplicationFactory<Startup>>, IClassFixture<RabbitMqFixture>
    {
        private readonly HttpClient client;
        private readonly RabbitMqFixture fixture;
        private readonly OrderDBContext dbcontext;
        private readonly GenericSqlServerRepository<Product, OrderDBContext> productrepo;
        private readonly GenericSqlServerRepository<Order, OrderDBContext> orderrepo;
        private Product product;

        public OrderServiceControllerTest(WebApplicationFactory<Startup> factory, RabbitMqFixture rabbitmqfixture)
        {
            // create a webapi app in memory and its client, so not IP is needed
            client = factory.CreateClient();
            var services = factory.Server.Host.Services;
            
            fixture = rabbitmqfixture;
            product = new Product() { Id = 1 };

            dbcontext = services.GetService(typeof(OrderDBContext)) as OrderDBContext;
            productrepo = new GenericSqlServerRepository<Product, OrderDBContext>(dbcontext);
            orderrepo = new GenericSqlServerRepository<Order, OrderDBContext>(dbcontext);
        }

        [Fact]
        public async void deleteitem()
        {
            var a = await this.client.DeleteAsync("api/product/1");
            a.StatusCode.Should().Be(200);
        }
        [Theory]
        [InlineData("api/product")]
        public async void Received_GivenNewProduct_ShouldPersistAndQuerable(string apiurl)
        {   //ARRANGE
            string queueName = null;
            var pc = new ProductCreated { Id = 1000, Category = 4, Name = "demo1", Price = new decimal(5.34) };
                // post a message 
            await fixture.PublishAsync<ProductCreated>(pc);

                // received a message
            var queueHandle = await fixture.SubscribeAndGetAsync<ProductCreated>(   //subscribe to rabbitmq 
                onMessageReceived: (@event, @taskcompletion) =>
                {
                    @taskcompletion.SetResult(@event);
                    return Task.CompletedTask;
                }
                , queueName: queueName);
            // persist to DB
            
            //ACT
            var createdEvent = await queueHandle.Task;

            product = await productrepo.FindByPrimaryKey(pc.Id);

            var productFromApi1 = await this.client.GetAsync(apiurl);
            var productFromApi = await this.client.GetAsync(apiurl + "/"+pc.Id);
            var productObjectFromApi = JsonConvert.DeserializeObject<Product>(productFromApi.Content.ReadAsStringAsync().Result);

            //ASSERT
            createdEvent.Id.Should().Be(pc.Id);                                  // Verify the rabbitmq
            createdEvent.Name.Should().Be(pc.Name);
            createdEvent.Price.Should().Be(pc.Price);
            createdEvent.Category.Should().Be(pc.Category);

            product.Id.Should().Be(pc.Id);                                       // Verify the DB
            product.Name.Should().Be(pc.Name);
            product.Price.Should().Be(pc.Price);
            product.ProductCategoryId.Should().Be(pc.Category);

            productFromApi.StatusCode.Should().Be(200);                         // Verify the API

            productObjectFromApi.Id.Should().Be(pc.Id);                          
            productObjectFromApi.Name.Should().Be(pc.Name);
            productObjectFromApi.Price.Should().Be(pc.Price);
            productObjectFromApi.ProductCategoryId.Should().Be(pc.Category);

        }
        //Given some orders are made on order subsytem, when user try to query all orders
        //made for a chosen product (or product category),
        //order subsystem should be able to display the result
        [Theory]
        [InlineData("api/order/")]
        public async void Get_OrdersBaseOnProduct_ShouldReturnCorrectOrders(string endpoint)
        {   //ARRANGE
            var param = new OrderQueryParams() {ProductId = 1};
            
            //ACT
            var response = await client.PostAsJsonAsync(endpoint, param); 
            var orders = JsonConvert.DeserializeObject<List<Order>>(response.Content.ReadAsStringAsync().Result);


            //ASSERT
            response.StatusCode.Should().Be(200);
            orders.Should().NotBeNull();
            orders.Count.Should().Be(1);
         }

        [Theory]
        [InlineData("api/order/", 4)]
        public async void Get_OrdersBaseOnProductCategory_ShouldReturnCorrectOrders(string endpoint, int categoryid)
        {   //ARRANGE
            var param = new OrderQueryParams() { ProductCategoryId = categoryid };

            //ACT
            var response = await client.PostAsJsonAsync(endpoint, param);
            var orders = JsonConvert.DeserializeObject<List<Order>>(response.Content.ReadAsStringAsync().Result);


            //ASSERT
            response.StatusCode.Should().Be(200);
            orders.Should().NotBeNull();
            orders.Count.Should().Be(1);
        }

        [Theory]
        [InlineData("api/order/", 5)]
        public async void Get_OrdersBaseOnProductCategory_ShouldReturnNoMatchingOrders(string endpoint, int categoryid)
        {   //ARRANGE
            var param = new OrderQueryParams() { ProductCategoryId = categoryid };

            //ACT
            var response = await client.PostAsJsonAsync(endpoint, param);
            var orders = JsonConvert.DeserializeObject<List<Order>>(response.Content.ReadAsStringAsync().Result);


            //ASSERT
            response.StatusCode.Should().Be(200);
            orders.Count.Should().Be(0);
        }

        //write xunit deconstructor 
        ~OrderServiceControllerTest()
        {
            //Teardown
            productrepo.DeleteModel(product);
            dbcontext.Dispose();
        }
                
        
    }
}
