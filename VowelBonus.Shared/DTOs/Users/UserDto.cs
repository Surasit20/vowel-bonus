namespace VowelBonus.Shared.DTOs;
public class UserDto
{
    public int UserId { get; init; }

    public string UserName { get; set; }

    public int TotalPoint { get; set; }

    public DateTimeOffset? LastLoginDate { get; set; }
}
