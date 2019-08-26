﻿using Microsoft.AspNetCore.Mvc;
using WeatherApp.Controllers;
using Xunit;

namespace WeatherApp.Test
{
    public class CitiesControllerTest
    {
        private readonly CitiesController _controller;

        [Fact]
        public void GetCities_NameExists_ReturnsOkResult()
        {
            var controller = new CitiesController(new FakeCitiesService());
            var result = controller.GetCities("existing");
            Assert.Single(result.Value);
        }

        [Fact]
        public void GetCities_NameDoesNotExists_ReturnsNotFoundResult()
        {
            var controller = new CitiesController(new FakeCitiesService());
            var result = controller.GetCities("non-existing");
            Assert.Empty(result.Value);
            
        }
    }
}
