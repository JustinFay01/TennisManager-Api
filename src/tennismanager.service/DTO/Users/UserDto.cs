namespace tennismanager.service.DTO;

public abstract class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    
    // TODO: Re-evaluate if these fields should be in the UserDto?
    public string? Nickname { get; set; }
    public string? Picture { get; set; } 
}