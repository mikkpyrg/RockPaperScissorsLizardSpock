using Domain.Models;

namespace Api.Dtos
{
    public class PlayResultDto
    {
        public PlayResultDto(PlayRound round)
        {
            Results = round.Result.ToString();
            Player = (int)round.PlayerChoice;
            Computer = (int)round.ChallengerChoice;
        }

        public string Results { get; set; }
        public int Player { get; set; }
        public int Computer { get; set; }
    }
}
