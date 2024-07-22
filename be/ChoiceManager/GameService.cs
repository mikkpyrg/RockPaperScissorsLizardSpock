using ChoiceManager.Dtos;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System.Net.Http.Json;

namespace ChoiceManager
{
    public class GameService : IGameService
    {
        private static readonly Dictionary<ChoiceType, ChoiceType[]> _winningMoves = new Dictionary<ChoiceType, ChoiceType[]>
            {
                { ChoiceType.Rock, [ChoiceType.Scissors, ChoiceType.Lizard] },
                { ChoiceType.Paper, [ChoiceType.Rock, ChoiceType.Spock] },
                { ChoiceType.Scissors, [ChoiceType.Paper, ChoiceType.Lizard] },
                { ChoiceType.Lizard, [ChoiceType.Paper, ChoiceType.Spock] },
                { ChoiceType.Spock, [ChoiceType.Rock, ChoiceType.Scissors] }
            };

        private readonly HttpClient _randomNumberClient;
        private readonly IPlayRoundRepository _playRoundRepostiory;

        public GameService(HttpClient randomNumberClient, IPlayRoundRepository playRoundRepository)
        {
            _randomNumberClient = randomNumberClient;
            _playRoundRepostiory = playRoundRepository;
        }

        public async Task<ChoiceType> GetRandomChoiceAsync()
        {
            var randomNumber = await GetRandomNumberAsync();

            // random number - 1, so the range of numbers are 0 - 19, 20 - 39 .. 80 - 99
            // that allows us to divide by 20 giving a range of 0 - 4
            // + 1 and we got a random enum value from 1 - 5
            var choiceIndex = ((randomNumber - 1) / 20) + 1;
            return (ChoiceType) choiceIndex;
        }

        public async Task<PlayRound> PlayAgainstCpuAsync(ChoiceType player, string playerName)
        {
            var challenger = await GetRandomChoiceAsync();

            var roundResult = new PlayRound
            {
                PlayerName = playerName,
                PlayerChoice = player,
                ChallengerName = "CPU",
                ChallengerChoice = challenger,
                Result = CompareChoices(player, challenger)
            };

            await _playRoundRepostiory.SaveAsync(roundResult);

            return roundResult;
        }

        public async Task<PlayRound> PlayAgainstChallengerAsync(ChoiceType player, string playerName)
        {
            var awaitingChallengedGame = await _playRoundRepostiory.GetChallengedGameAsync(playerName);

            if (awaitingChallengedGame == null)
            {
                var roundResult = new PlayRound
                {
                    PlayerName = playerName,
                    PlayerChoice = player
                };
                await _playRoundRepostiory.SaveAsync(roundResult);
                return roundResult;
            }
            else
            {
                awaitingChallengedGame.ChallengerName = playerName;
                awaitingChallengedGame.ChallengerChoice = player;
                awaitingChallengedGame.Result = CompareChoices(awaitingChallengedGame.PlayerChoice, player);
                await _playRoundRepostiory.SaveAsync(awaitingChallengedGame);
                return awaitingChallengedGame;
            }
        }

        public async Task<IEnumerable<PlayRound>> GetPlayHistoryAsync(string playerName)
        {
            return await _playRoundRepostiory.GetHistoryAsync(playerName);
        }

        public async Task<IEnumerable<PlayRound>> GetLatestPlaysAsync()
        {
            return await _playRoundRepostiory.GetLatestAsync();
        }

        private PlayResultType CompareChoices(ChoiceType player, ChoiceType challenger)
        {
            if (player == challenger)
            {
                return PlayResultType.Tie;
            }

            return _winningMoves[player].Contains(challenger) ? PlayResultType.Win : PlayResultType.Lose;
        }

        private async Task<int> GetRandomNumberAsync()
        {
            try
            {
                var result = await _randomNumberClient.GetFromJsonAsync<RandomNumberClientResponse>(_randomNumberClient.BaseAddress);

                if (result == null)
                {
                    throw new Exception("Random service API returned null");
                }

                // 1 to 100
                return result.random_number;
            }
            catch (Exception e)
            {
                var random = new Random();
                // min is inclusive, max is not
                return random.Next(1, 101);
            }
        }
    }
}
