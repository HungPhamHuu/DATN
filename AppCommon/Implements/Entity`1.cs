using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppCommon.Interfaces;

namespace AppCommon.Implements;

public abstract class Entity<TKey> : Entity, IEntity<TKey>, IEntity
{
    [Key, Column(Order = 0),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual TKey Id { get;set; }
}