using Api.Dtos;
using Domain.Enums;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("choices")]
        public IEnumerable<ChoiceDto> GetChoices()
        {
            return Enum.GetValues(typeof(ChoiceType))
                .Cast<ChoiceType>()
                .Select(x => new ChoiceDto(x));
        }

        [HttpGet("choice")]
        public async Task<ChoiceDto> GetRandomChoiceAsync()
        {
            var randomChoice = await _gameService.GetRandomChoiceAsync();
            return new ChoiceDto(randomChoice);
        }

        [HttpPost("play")]
        public async Task<PlayResultDto> PlayAgainstCpuAsync(PlayRequestDto dto)
        {
            var playResult = await _gameService.PlayAgainstCpuAsync((ChoiceType)dto.Player, GetPlayerName());
            return new PlayResultDto(playResult);
        }

        [HttpPost("play-multiplayer")]
        public async Task<PlayHistoryDto> PlayAgainstChallengerAsync(PlayRequestDto dto)
        {
            var name = GetPlayerName();
            var playResult = await _gameService.PlayAgainstChallengerAsync((ChoiceType)dto.Player, name);
            return MapToDto(playResult, name);
        }

        [HttpGet("history")]
        public async Task<IEnumerable<PlayHistoryDto>> GetPlayHistoryAsync()
        {
            var name = GetPlayerName();
            var playResult = await _gameService.GetPlayHistoryAsync(name);
            return playResult.Select(x => MapToDto(x, name));
        }

        [HttpGet("latest")]
        public async Task<IEnumerable<PlayHistoryDto>> GetLatestPlaysAsync()
        {
            var playResult = await _gameService.GetLatestPlaysAsync();
            return playResult.Select(MapToDto);
        }

        private string GetPlayerName()
        {
            Request.Headers.TryGetValue("name", out var name);
            var foundName = name.FirstOrDefault() ?? string.Empty;
            return foundName.Equals("cpu", StringComparison.InvariantCultureIgnoreCase) ? String.Empty : foundName;
        }

        public static PlayResultType ReverseResultType(PlayResultType result) => result switch
        {
            PlayResultType.Win => PlayResultType.Lose,
            PlayResultType.Lose => PlayResultType.Win,
            _ => PlayResultType.Tie,
        };

        private PlayHistoryDto MapToDto(PlayRound round, string name)
        {
            if (round.ChallengerName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
            {
                return new PlayHistoryDto
                {
                    ChallengerName = round.PlayerName,
                    PlayerName = round.ChallengerName,
                    ChallengerChoice = round.PlayerChoice.ToString(),
                    PlayerChoice = round.ChallengerChoice?.ToString(),
                    Result = round.Result == null ? null : ReverseResultType(round.Result.Value).ToString()
                };
            }
            else
            {
                return MapToDto(round);
            }
        }

        private PlayHistoryDto MapToDto(PlayRound round)
        {
            return new PlayHistoryDto
            {
                ChallengerName = round.ChallengerName,
                PlayerName = round.PlayerName,
                ChallengerChoice = round.ChallengerChoice?.ToString(),
                PlayerChoice = round.PlayerChoice.ToString(),
                Result = round.Result?.ToString()
            };
        }
    }
}
