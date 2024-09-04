using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

public class CustomerSession : BaseEntity
{
    
    public Customer Customer { get; set; }
    public Guid CustomerId { get; set; }
    
    public Session Session { get; set; }
    public Guid SessionId { get; set; }

    /// <summary>
    ///     The actual date of the session.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    ///     Overrides the default price of the session. Allows for a special price to be set for a customer.
    /// </summary>
    public decimal? CustomPrice { get; set; }
}

public class CustomerSessionEntityTypeConfiguration : IEntityTypeConfiguration<CustomerSession>
{
    public void Configure(EntityTypeBuilder<CustomerSession> builder)
    {        
    }
}