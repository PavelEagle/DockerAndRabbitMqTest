using System.Threading.Tasks;

namespace Consumer.Core.Abstractions.History
{
    public interface IHistoryService
    {
        Task AddHistoryAsync(Models.History history);
        Task<Models.History> GetHistoryAsync(int id);
    }
}