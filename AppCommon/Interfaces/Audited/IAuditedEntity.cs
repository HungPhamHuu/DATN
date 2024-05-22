
namespace AppCommon.Interfaces.Audited;

public interface IAuditedEntity :
ICreationAuditedEntity,
IModificationAuditedEntity,
IDeletionAuditedEntity,
IEntity
{

}