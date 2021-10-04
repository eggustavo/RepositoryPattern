namespace EGG.RepositoryPattern.Domain.Entities.Base
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; protected set; }
    }
}
