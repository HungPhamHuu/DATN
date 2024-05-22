namespace AppCommon.Interfaces.Audited;

public interface IDeletionAuditedEntity
{
    bool IsDeleted { get; }

    Guid? DeletedBy { get; }

    DateTimeOffset DeletedTime { get; }
}