using System;
using System.Threading.Tasks;
using EventBus;
using EventBus.Abstractions;
using EventBus.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Producer.Models;

namespace Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestEventController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly BusConfiguration _busConfiguration;
        
        public TestEventController(IBus bus, IOptions<BusConfiguration> options)
        {
            _bus = bus;
            _busConfiguration = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> TestAsync(PublishRequest request)
        {
            var testEvent = new TestEvent
            {
                Description = request.Description,
                Title = request.Title
            };
            
            await _bus.SendAsync(testEvent, _busConfiguration.Host, QueueNames.TestQueue);

            return Ok();
        }
    }
}