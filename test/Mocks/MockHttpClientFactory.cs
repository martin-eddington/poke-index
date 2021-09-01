using Moq;  
using Moq.Protected;  
using Newtonsoft.Json;  
using System;  
using System.Net;  
using System.Net.Http;  
using System.Threading;  
using System.Threading.Tasks;  
using Xunit;  
using Microsoft.AspNetCore.Mvc;
using PokeIndex.Controllers;


namespace Mocks
{
    public class MockHttpClientFactory
    {
        public Mock<IHttpClientFactory> Factory(HttpStatusCode statusCode, string content)
        {
          
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            
            // Mock the client response
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(content)            
                });
 
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://dummy.URL");
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            return mockFactory;
        }    
    }
}