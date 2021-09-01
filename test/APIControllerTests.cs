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
using Mocks;
using PokeIndex.Test.Constants;

namespace test
{
    public class APIControllerTests
    {
        [Fact]
        public void Test1()
        {
            var mockFactory = new MockHttpClientFactory().Factory(HttpStatusCode.OK, Responses.POKEMON);
            // Act
            DefaultApiController controller = new DefaultApiController(mockFactory.Object);
            var result = controller.ShowPokemonByName("Test") as ObjectResult;
             //Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK,(HttpStatusCode)result.StatusCode);
        }


    }
}
