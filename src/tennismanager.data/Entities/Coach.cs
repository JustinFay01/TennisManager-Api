using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

public class Coach : User
{
    public decimal HourlyRate { get; set; }
}