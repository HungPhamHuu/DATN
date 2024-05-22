using AppCommon.Implements;
using AppCommon.Interfaces.Audited;
using AppCommon.Interfaces;

namespace AppCommonImplements;

public class FullAuditedEntity :
    Entity,
    IAuditedEntity,
    ICreationAuditedEntity,
    IModificationAuditedEntity,
    IDeletionAuditedEntity,
    IEntity
{
    public DateTimeOffset CreatedTime { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset ModifiedTime { get; set; }
    public Guid? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTimeOffset DeletedTime { get; set; }
}