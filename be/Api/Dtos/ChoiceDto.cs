using Domain.Enums;

namespace Api.Dtos
{
    public class ChoiceDto
    {
        public ChoiceDto(ChoiceType type)
        {
            Id = (int)type;
            Name = type.ToString();
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
