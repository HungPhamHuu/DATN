namespace AppCommon.Interfaces;

public interface IEntity<TKey> : IEntity
{
    TKey Id { get; }
}