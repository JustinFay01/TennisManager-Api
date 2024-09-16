namespace tennismanager.api.Models.Session;

public class CustomerSessionRequest
{
    public Guid CustomerId { get; set; }
    public Guid SessionId { get; set; }
    public DateTime Date { get; set; }
    public decimal? CustomPrice { get; set; }
}