using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PlayRoundRepository : IPlayRoundRepository
    {
        private readonly EntityDbContext _context;

        public PlayRoundRepository(EntityDbContext context)
        {
            _context = context;
        }
        public async Task SaveAsync(PlayRound entity)
        {
            _context.PlayRounds.Update(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<PlayRound> GetChallengedGameAsync(string name)
        {
            return await _context.PlayRounds.FirstOrDefaultAsync(x => x.ChallengerChoice == null && !x.PlayerName.Equals(name));
        }

        public async Task<IEnumerable<PlayRound>> GetHistoryAsync(string name)
        {
            return await _context.PlayRounds
                .Where(x => x.PlayerName.Equals(name) || x.ChallengerName.Equals(name))
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<PlayRound>> GetLatestAsync()
        {
            return await _context.PlayRounds
                .Where(x => x.ChallengerChoice != null)
                .OrderByDescending(x => x.Id)
                .Take(10)
                .ToListAsync();
        }
    }
}
