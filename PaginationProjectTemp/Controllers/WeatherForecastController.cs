﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaginationHelper.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginationProjectTemp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IPaginationHelper pagination;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPaginationHelper paginationHelper)
        {
            _logger = logger;
            pagination = paginationHelper;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("pagination")]
        public IActionResult GetPagination()
        {
            var rng = new Random();
            var route = Request.Path.Value;
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();
            var filter = new Dictionary<string, string>();
            filter.Add("status", "true");
            filter.Add("firstName", "A");
            var filters = new PaginationFilter(1,10, filter);
            return Ok(pagination.CreatePagedReponse<WeatherForecast>(data, 112, route, Request,filters));
        }
    }
}
