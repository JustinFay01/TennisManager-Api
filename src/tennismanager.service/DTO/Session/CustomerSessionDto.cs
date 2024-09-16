namespace tennismanager.service.DTO.Session;

public class CustomerSessionDto
{
    public Guid CustomerId { get; set; }
    public Guid SessionId { get; set; }
    public DateTime Date { get; set; }
    public decimal? CustomPrice { get; set; }
}