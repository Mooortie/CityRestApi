using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using CityRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CityRestApi
{
    public static class CityApi
    {
        private static List<City> cities = new();

        //Get all cities
        [FunctionName("GetCity")]
        public static async Task<IActionResult> GetCity(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cityitems")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get all cities");

            return new OkObjectResult(cities);
        }

        //Get all cities by id
        [FunctionName("GetCityById")]
        public static async Task<IActionResult> GetCityById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cityitems/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation("Get city by id");

            var citiesItem = cities.Find(x=>x.Id==id);
            
            if (citiesItem == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(citiesItem);
        }

        //Create new city
        [FunctionName("CreateCity")]
        public static async Task<IActionResult> PostCity(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cityitems")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Create city");
            
            string requestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<CreateCity>(requestData);

            var city = new City
            {
                CityName = data.CityName,
                PostalCode = data.PostalCode,
            };

            cities.Add(city);
            return new OkObjectResult(city);
        }

        //Update city
        [FunctionName("UpdateCity")]
        public static async Task<IActionResult> PutCity(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "cityitems/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation("Create city");

            var citiesItem = cities.Find(x => x.Id == id);

            if (citiesItem == null)
            {
                return new NotFoundResult();
            }

            string requestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UpdateCity>(requestData);
            citiesItem.CityName = data.CityName;
            citiesItem.Visited = data.Visited;
            return new OkObjectResult(citiesItem);
        }

        //Delete city
        [FunctionName("DeleteCity")]
        public static async Task<IActionResult> DeleteCity(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "cityitems/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation("Delete city");

            var citiesItem = cities.Find(x => x.Id == id);

            if (citiesItem == null)
            {
                return new NotFoundResult();
            }

            cities.Remove(citiesItem);
            return new OkResult();
        }
    }
}
