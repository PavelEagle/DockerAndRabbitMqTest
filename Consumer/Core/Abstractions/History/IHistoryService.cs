using System.Threading.Tasks;

namespace Consumer.Core.Abstractions.History
{
    public interface IHistoryService
    {
        Task AddHistoryAsync(History history);
    }
}