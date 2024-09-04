namespace tennismanager.data.Entities.Abstract;

public abstract class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}

public class Admin : User
{
    public string Username { get; set; }
    public string Password { get; set; }
}