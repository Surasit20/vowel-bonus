using VowelBonus.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VowelBonus.Domain.Entities
{
    [Table("VowelBonusScoreHistory")]
    public class VowelBonusScoreHistory : BaseAuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(@"VowelBonusScoreHistoryId", Order = 1)]
        [Required]
        public int VowelBonusScoreHistoryId { get; set; } // VowelBonusScoreHistoryId (Primary key)

        [Column("UserId", Order = 2)]
        [Required]
        public int UserId { get; set; } // Foreign key

        [Column(@"Word", Order = 3)]
        [Required]
        [MaxLength(400)]
        public required string Word { get; set; } // Word (length: 400)

        [Column(@"Point", Order = 4)]
        [Required]
        public int Point { get; set; } // Point (int)

        public User User { get; set; } = null!;

    }
}
