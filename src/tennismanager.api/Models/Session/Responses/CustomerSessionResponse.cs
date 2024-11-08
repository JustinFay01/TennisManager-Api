namespace tennismanager.api.Models.Session.Responses;

public class CustomerSessionResponse
{
    public Guid CustomerId { get; set; }
    public Guid SessionId { get; set; }
    public DateTime Date { get; set; }
    public decimal? CustomPrice { get; set; }
}