using System.Threading.Tasks;
using Consumer.Core.Abstractions.History;
using Consumer.Core.Abstractions.History.Models;
using EventBus.Abstractions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Consumer.Bus.Consumers
{
    public class HistoryEventConsumer: IConsumer<HistoryEvent>
    {
        private readonly IHistoryService _historyService;
        private readonly ILogger<HistoryEventConsumer> _logger;

        public HistoryEventConsumer(IHistoryService historyService, ILogger<HistoryEventConsumer> logger)
        {
            _historyService = historyService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<HistoryEvent> context)
        {
            await _historyService.AddHistoryAsync(new History
            {
                Title = context.Message.Title,
                Description = context.Message.Description
            });
            
            _logger.LogInformation($"{nameof(HistoryEvent)} was saved");
        }
    }
}