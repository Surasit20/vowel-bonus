using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VowelBonus.Shared.DTOs;
public class VowelBonusScoreHistoryFilterDto: BaseFilterDto
{
    public int UserId { get; set; }

    //Filter
    public string? StartWord { get; set; }
    public string? EndWord { get; set; }
    public int? StartPoint { get; set; }
    public int? EndPoint { get; set; }
}