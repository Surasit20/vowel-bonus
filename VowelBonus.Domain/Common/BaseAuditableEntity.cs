namespace VowelBonus.Domain.Common;

public abstract class BaseAuditableEntity 
{
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset LastModifiedDate { get; set; }
    public bool IsDelete { get; set; }
    public bool IsActive { get; set; }
}
