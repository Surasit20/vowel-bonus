using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VowelBonus.Shared.DTOs;
public class VowelBonusScoreHistorySaveDto
{
    public int UserId { get; set; }
    public string Word { get; set; } 
}
