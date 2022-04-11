using Microsoft.AspNetCore.Mvc.Testing;
using OnlineCoursesWeb;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OnlineCoursesTest
{
    public class UnitTest1:IClassFixture<WebApplicationFactory<OnlineCoursesWeb.Startup>>
    {
        private readonly WebApplicationFactory<OnlineCoursesWeb.Startup> _factory;
        
        public UnitTest1(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task TestIndexPage()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Home/Index");
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Index")]
        [InlineData("/Home/Privacy")]
        [InlineData("/Home/FAQ")]
        [InlineData("/Identity/Account/Login")]
        [InlineData("/Identity/Account/Register")]
        [InlineData("/Teachers/Index")]
        [InlineData("/Teachers/Details/1")]
        [InlineData("/Teachers/Edit/1")]
        [InlineData("/Teachers/Create")]
        public async Task TestAllPages(string url)
        {
            //Arrange
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);
            int code = (int)response.StatusCode;

            Assert.Equal(200, code);
        }
    }
}
