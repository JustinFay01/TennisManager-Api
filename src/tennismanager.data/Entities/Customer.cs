using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

public class Customer : User
{
    public ICollection<CustomerSession> Sessions { get; set; }
}