﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager.data;
using tennismanager.data.Entities;
using tennismanager.data.Entities.Abstract;
using tennismanager.service.DTO;
using tennismanager.shared.Exceptions.Exceptions;

namespace tennismanager.service.Services;

public interface IUserService
{
    public Task<UserDto?> CreateUserAsync(UserDto userDto);

    public Task<UserDto?> GetUserAsync(Guid id);
    public Task<List<UserDto>> GetUsersAsync();
    public Task DeleteUserAsync(Guid id);

    public Task<UserDto?> GetUserByAuth0Sub(string auth0Id);
}

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly TennisManagerContext _tennisManagerContext;

    public UserService(IMapper mapper, TennisManagerContext tennisManagerContext)
    {
        _mapper = mapper;
        _tennisManagerContext = tennisManagerContext;
    }

    public async Task<UserDto?> CreateUserAsync(UserDto userDto)
    {
        var entity = _mapper.Map<User>(userDto);

        _tennisManagerContext.Users.Add(entity);
        await _tennisManagerContext.SaveChangesAsync();

        var result = _mapper.Map<UserDto>(entity);
        return result;
    }

    public async Task<UserDto?> GetUserAsync(Guid id)
    {
        var user = await _tennisManagerContext.Users.FindAsync(id);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        var users = await _tennisManagerContext.Users.ToListAsync();
        var result = _mapper.Map<List<UserDto>>(users);
        return result;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _tennisManagerContext.Users.FindAsync(id);
        if (user is null) throw new UserNotFoundException();
        
        _tennisManagerContext.Users.Remove(user);
        await _tennisManagerContext.SaveChangesAsync();
    }

    public async Task<UserDto?> GetUserByAuth0Sub(string auth0Id)
    {
        var user = await _tennisManagerContext.Users.FirstOrDefaultAsync(u => u.Sub == auth0Id);
        return user is null ? null : _mapper.Map<UserDto>(user);
    }
}