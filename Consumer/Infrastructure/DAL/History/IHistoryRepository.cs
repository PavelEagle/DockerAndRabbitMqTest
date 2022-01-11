using System.Threading.Tasks;

namespace Consumer.Infrastructure.DAL.History
{
    public interface IHistoryRepository
    {
        Task AddHistoryAsync(Core.Abstractions.History.Models.History history);
        Task<Core.Abstractions.History.Models.History> GetHistoryAsync(int id);
    }
}