using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VowelBonus.Shared.DTOs;
public class VowelBonusScoreHistoryResponseDto
{
    public int TotalPoint { get; set; }
    public IEnumerable<VowelBonusScoreHistoryDto> VowelBonusScoreHistories { get; set; } = new List<VowelBonusScoreHistoryDto>();
}
