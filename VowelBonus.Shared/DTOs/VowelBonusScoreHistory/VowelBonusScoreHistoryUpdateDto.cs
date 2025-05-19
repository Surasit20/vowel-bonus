using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VowelBonus.Shared.DTOs;
public class VowelBonusScoreHistoryUpdateDto
{
    public int VowelBonusScoreHistoryId { get; set; }
    public string Word { get; set; } 
}
