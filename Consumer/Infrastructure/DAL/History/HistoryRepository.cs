using System.Linq;
using System.Threading.Tasks;
using Consumer.Infrastructure.Context;
using Consumer.Infrastructure.DAL.History.Models;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Infrastructure.DAL.History
{
    public class HistoryRepository: IHistoryRepository
    {
        private readonly ConsumerContext _context;

        public HistoryRepository(ConsumerContext context)
        {
            _context = context;
        }

        public async Task AddHistoryAsync(Core.Abstractions.History.Models.History history)
        {
            var dbModel = new DbHistory
            {
                Title = history.Title,
                Description = history.Description
            };
            
            await _context.History.AddAsync(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task<Core.Abstractions.History.Models.History> GetHistoryAsync(int id)
        {
            var history = await _context.History
                .Where(x=>x.Id == id)
                .FirstOrDefaultAsync();
            
            return history != null ? new Core.Abstractions.History.Models.History
            {
                Title = history.Title,
                Description = history.Description,
                CreatedAt = history.CreatedAt
            }: null;
        }
    }
}