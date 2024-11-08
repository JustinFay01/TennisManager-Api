﻿namespace tennismanager.api.Models.User.Abstract;

//TODO: Mark abstract when Customer Fields are added to Customer Response
public abstract class UserResponse
{
    public Guid Id { get; set; }
    public string Type { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Nickname { get; set; }
    public string? Picture { get; set; } 
}
