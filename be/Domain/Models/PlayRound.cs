using Domain.Enums;

namespace Domain.Models
{
    public class PlayRound
    {
        public int Id { get; set; }
        public string PlayerName { get; set; } = "";
        public ChoiceType PlayerChoice { get; set; }
        public string ChallengerName { get; set; } = "";
        public ChoiceType? ChallengerChoice { get; set; }
        public PlayResultType? Result { get; set; }
    }
}
