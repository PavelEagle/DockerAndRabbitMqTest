using System.Threading.Tasks;
using EventBus;
using EventBus.Abstractions;
using EventBus.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Producer.Models;

namespace Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IBus _bus;

        public HistoryController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> PublishHistoryAsync(PublishRequest request)
        {
            var testEvent = new HistoryEvent
            {
                Description = request.Description,
                Title = request.Title
            };

            await _bus.SendAsync(testEvent, QueueNames.TestQueue);

            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHistoryAsync(int id)
        {
            var result = await _bus.GetResponseAsync<HistoryRequest, HistoryResponse>(
                new HistoryRequest {Id = id},
                QueueNames.TestRequestQueue);

            // TODO fix it on middleware exceptions
            if (result == null)
                return NotFound($"History with id = {id} not found");
            
            return Ok(result);
        }
    }
}