namespace tennismanager_api.tennismanager.data.Entities.Abstract;

public interface IAuditable
{
    DateTime CreatedOn { get; set; }
    DateTime? UpdatedOn { get; set; }
    Guid CreatedById { get; set; }
    User CreatedBy { get; set; }
    Guid? UpdatedById { get; set; }
    User? UpdatedBy { get; set; }
}