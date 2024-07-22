using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Api.Dtos
{
    public class PlayHistoryDto
    {
        public string Result { get; set; }
        public string PlayerChoice { get; set; }
        public string PlayerName { get; set; }
        public string ChallengerChoice { get; set; }
        public string ChallengerName { get; set; }
    }
}
