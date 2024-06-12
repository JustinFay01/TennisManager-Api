namespace tennismanager.data.Entities.Abstract;

public abstract class BaseEntity<T> : IBaseEntity<T>
{
    public T Id { get; set; }
}