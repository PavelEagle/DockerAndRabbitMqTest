using System.Threading.Tasks;
using Consumer.Core.Abstractions.History;
using Consumer.Core.Abstractions.History.Models;
using Consumer.Infrastructure.DAL.History;

namespace Consumer.Core.Services
{
    public class HistoryService: IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public Task AddHistoryAsync(History history) => _historyRepository.AddHistoryAsync(history);
        public Task<History> GetHistoryAsync(int id) => _historyRepository.GetHistoryAsync(id);
    }
}