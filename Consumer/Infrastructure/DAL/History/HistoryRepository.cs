using System.Threading.Tasks;
using Consumer.Infrastructure.Context;
using Consumer.Infrastructure.DAL.History.Models;

namespace Consumer.Infrastructure.DAL.History
{
    public class HistoryRepository: IHistoryRepository
    {
        private readonly ConsumerContext _context;

        public HistoryRepository(ConsumerContext context)
        {
            _context = context;
        }

        public async Task AddHistoryAsync(Core.Abstractions.History.History history)
        {
            var dbModel = new DbHistory
            {
                Title = history.Title,
                Description = history.Description
            };
            
            await _context.History.AddAsync(dbModel);
            await _context.SaveChangesAsync();
        }
    }
}