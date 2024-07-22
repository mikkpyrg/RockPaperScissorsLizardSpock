using Domain.Enums;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IGameService
    {
        Task<ChoiceType> GetRandomChoiceAsync();
        Task<PlayRound> PlayAgainstCpuAsync(ChoiceType playerChoice, string playerName);
        Task<PlayRound> PlayAgainstChallengerAsync(ChoiceType playerChoice, string playerName);
        Task<IEnumerable<PlayRound>> GetPlayHistoryAsync(string playerName);
        Task<IEnumerable<PlayRound>> GetLatestPlaysAsync();
    }
}
