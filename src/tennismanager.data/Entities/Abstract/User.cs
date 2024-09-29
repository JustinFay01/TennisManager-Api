namespace tennismanager.data.Entities.Abstract;

public abstract class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    
    // Auth0 Fields
    public string? Nickname { get; set; }
    public string? Picture { get; set; }
    public string? Sub { get; set; }
    public string? Auth0Provider => !string.IsNullOrEmpty(Sub) ? Sub.Split("|")[0] : null;
    public string? Auth0Id => !string.IsNullOrEmpty(Sub) ? Sub.Split("|")[1] : null;
}

public class Admin : User
{
    public string Username { get; set; }
    public string Password { get; set; }
}