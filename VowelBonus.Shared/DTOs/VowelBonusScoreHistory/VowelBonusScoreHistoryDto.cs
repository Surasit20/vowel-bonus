using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VowelBonus.Shared.DTOs;
public class VowelBonusScoreHistoryDto
{
    public int VowelBonusScoreHistoryId { get; set; }
    public string Word { get; set; } 
    public int Point { get; set; } 
    public DateTimeOffset? CreatedDate { get; set; }
}
