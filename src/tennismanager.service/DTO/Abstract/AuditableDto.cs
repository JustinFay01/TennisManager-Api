namespace tennismanager.service.DTO.Abstract;

public interface IAuditableDto<T>
{
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public T CreatedById { get; set; }
    public T? UpdatedById { get; set; }
}

public abstract class AuditableDto<T> : IAuditableDto<T>
{
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public T CreatedById { get; set; }
    public T? UpdatedById { get; set; }
}