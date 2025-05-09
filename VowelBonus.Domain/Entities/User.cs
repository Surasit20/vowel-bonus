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
    [Table("User")]
    public class User : BaseAuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(@"UserId", Order = 1)]
        [Required]
        public int UserId { get; set; } // UserId (Primary key)

        [Column(@"UserName", Order = 2)]
        [Required]
        [MaxLength(50)]
        public required string UserName { get; set; } // UserName (length: 50)

        [Column("LastLoginDate", Order = 3)]
        public DateTimeOffset? LastLoginDate { get; set; } // LastLoginDate (nullable)

        [Column("LastLogoutDate", Order = 4)]
        public DateTimeOffset? LastLogoutDate { get; set; } // LastLogoutDate (nullable)

        public virtual ICollection<VowelBonusScoreHistory> VowelBonusScoreHistories { get; set; } = new List<VowelBonusScoreHistory>();

    }
}
