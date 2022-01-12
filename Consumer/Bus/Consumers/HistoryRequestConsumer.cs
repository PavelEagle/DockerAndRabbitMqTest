using System.Threading.Tasks;
using Consumer.Core.Abstractions.History;
using EventBus.Abstractions;
using MassTransit;

namespace Consumer.Bus.Consumers
{
    public class HistoryRequestConsumer : IConsumer<HistoryRequest>
    {
        private readonly IHistoryService _historyService;

        public HistoryRequestConsumer(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task Consume(ConsumeContext<HistoryRequest> context)
        {
            var history = await _historyService.GetHistoryAsync(context.Message.Id);

            if (history == null)
            {
                await context.RespondAsync<HistoryNotFound>(new
                {
                    Message = $"{typeof(HistoryNotFound)} not found"
                });
            }
            else
            {
                await context.RespondAsync<HistoryResponse>(new
                {
                    history.Title,
                    history.Description,
                    history.CreatedAt
                });
            }
        }
    }
}