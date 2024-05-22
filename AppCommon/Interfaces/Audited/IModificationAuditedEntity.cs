namespace AppCommon.Interfaces.Audited;

public interface IModificationAuditedEntity
{

    DateTimeOffset ModifiedTime { get; }

    Guid? ModifiedBy { get; }
}