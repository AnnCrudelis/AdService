using WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using System.Net.Http;
using WebApi.Filter;
using WebApi.Wrappers;

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
        public async Task Get_ShouldReturnList()
        {
            var response = await _fixture.Client.GetAsync("/api/ad?pageNumber=1&pageSize=10&orderBy=Price");
            var model = JsonConvert.DeserializeObject<List<PagedResponse<Ad>>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(model);
        }

        [Fact]
        public async Task Get_ShouldReturnOk()
        {
            var response = await _fixture.Client.GetAsync("/api/ad?pageNumber=1&pageSize=10&orderBy=Price");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk()
        {
            var response = await _fixture.Client.GetAsync("/api/ad/2");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
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

        [Fact]
        public async void Delete_ReturnsHttpCodeOk()
        {
            var responseDelete = await _fixture.Client.DeleteAsync("api/ad/1");

            Assert.Equal(System.Net.HttpStatusCode.OK, responseDelete.StatusCode);
        }

        [Fact]
        public async void Delete_ReturnsHttpCode404()
        {
            var responseDelete = await _fixture.Client.DeleteAsync("api/ad/100");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, responseDelete.StatusCode);
        }

        [Fact]
        public async void DeleteAndGet_ReturnsHttpCode404()
        {
            var responseDelete = await _fixture.Client.DeleteAsync("api/ad/3");
            var responseGet = await _fixture.Client.GetAsync("/api/ad/3");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, responseGet.StatusCode);
        }

    }
}
