using AppCommon.Interfaces.Audited;
using AppCommon.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AppCommonImplements;

namespace AppCommon.Implements;

public class FullAuditedEntity<TKey> :
    FullAuditedEntity,
    IAuditedEntity<TKey>,
    ICreationAuditedEntity,
    IModificationAuditedEntity,
    IDeletionAuditedEntity,
    IEntity<TKey>,
    IEntity
{
    [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey Id { get; set; }
}