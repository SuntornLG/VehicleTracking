using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VehicleTracking.Controllers
{
    [Route("v1/Redis")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IEasyCachingProvider cachingProvider;
        private readonly IEasyCachingProviderFactory cachingProviderFactory;

        public RedisController(IEasyCachingProviderFactory cachingProviderFactory)
        {
            this.cachingProviderFactory = cachingProviderFactory;
            this.cachingProvider = this.cachingProviderFactory.GetCachingProvider("redis1");
        }

        [HttpGet("Set")]
        public IActionResult SetItemInQueue()
        {
            this.cachingProvider.Set("TestKey123", "Here is my value", TimeSpan.FromMinutes(2));

            return Ok();
        }

        [HttpGet("Get")]
        public IActionResult GetItemInQueue()
        {
            var item = this.cachingProvider.Get<string>("TestKey123");

            return Ok(item);
        }

    }
}