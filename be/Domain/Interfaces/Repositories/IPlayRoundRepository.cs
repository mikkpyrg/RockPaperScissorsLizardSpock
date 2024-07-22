using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IPlayRoundRepository
    {
        Task SaveAsync(PlayRound entity);
        Task<PlayRound> GetChallengedGameAsync(string name);
        Task<IEnumerable<PlayRound>> GetLatestAsync();
        Task<IEnumerable<PlayRound>> GetHistoryAsync(string name);
    }
}
