using System.Threading.Tasks;

namespace Consumer.Infrastructure.DAL.History
{
    public interface IHistoryRepository
    {
        Task AddHistoryAsync(Core.Abstractions.History.History history);
    }
}