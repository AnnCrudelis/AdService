using WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using System.Net.Http;

namespace ApiTest
{
    public class UnitTest1 : IClassFixture<BaseTestServerFixture>
    {
        private readonly BaseTestServerFixture _fixture;

        public UnitTest1(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            var response = await _fixture.Client.GetAsync("/api/ad");
            response.EnsureSuccessStatusCode();
            var models = JsonConvert.DeserializeObject<IEnumerable<Ad>>(await response.Content.ReadAsStringAsync());

            Assert.NotEmpty(models);
        }
        [Fact]
        public async Task Get_ShouldReturnListResultWithSort()
        {
            var response = await _fixture.Client.GetAsync("/api/ad/Price/true");
            response.EnsureSuccessStatusCode();
            var models = JsonConvert.DeserializeObject<IEnumerable<Ad>>(await response.Content.ReadAsStringAsync());

            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task GetById_ShouldReturnOneResult()
        {
            var response = await _fixture.Client.GetAsync("/api/ad/1");
            response.EnsureSuccessStatusCode();
            var models = JsonConvert.DeserializeObject<Ad>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(models);
        }

        [Fact]
        public async void GetById_WhenUnknownId_Returns404()
        {
            var response = await _fixture.Client.GetAsync("/api/ad/100");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void Post_ReturnsHttpCodeOk()
        {
            Ad ad = new Ad { Name = "Alert!", Date = DateTime.Now, Price = 1111, Photo = "123" };
            string json = JsonConvert.SerializeObject(ad);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _fixture.Client.PostAsync("api/ad", httpContent);

            Assert.Equal(System.Net.HttpStatusCode.OK,response.StatusCode);
        }

        [Fact]
        public async void Post_ReturnsHttpCode400()
        {
            Ad ad = new Ad { Date = DateTime.Now, Price = 1111 };
            string json = JsonConvert.SerializeObject(ad);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _fixture.Client.PostAsync("api/ad", httpContent);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}
