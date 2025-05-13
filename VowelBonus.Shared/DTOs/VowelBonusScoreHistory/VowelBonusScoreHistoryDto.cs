using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VowelBonus.Shared.DTOs;
public class VowelBonusScoreHistoryDto
{
    public string Word { get; set; } // Word (length: 400)
    public int Point { get; set; } // Point (int)
    public DateTimeOffset? CreatedDate { get; set; }
}
