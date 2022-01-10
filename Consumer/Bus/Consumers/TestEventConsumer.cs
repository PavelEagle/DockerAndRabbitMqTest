using System.Threading.Tasks;
using Consumer.Core.Abstractions.History;
using EventBus.Abstractions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Consumer.Bus.Consumers
{
    public class TestEventConsumer: IConsumer<TestEvent>
    {
        private readonly IHistoryService _historyService;
        private readonly ILogger<TestEventConsumer> _logger;

        public TestEventConsumer(IHistoryService historyService, ILogger<TestEventConsumer> logger)
        {
            _historyService = historyService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<TestEvent> context)
        {
            await _historyService.AddHistoryAsync(new History
            {
                Title = context.Message.Title,
                Description = context.Message.Description
            });
            
            _logger.LogInformation($"{nameof(TestEvent)} was saved");
        }
    }
}