using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager.data;
using tennismanager.data.Entities;
using tennismanager.data.Entities.Abstract;
using tennismanager.service.DTO;

namespace tennismanager.service.Services;

public interface IUserService
{
    public Task<UserDto?> GetUserByAuth0Sub(string auth0Id);

    public Task<UserDto?> CreateUserAsync(UserDto userDto);

    public Task<UserDto?> GetUserByIdAsync(Guid id);
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
    
    public async Task<UserDto?> GetUserByAuth0Sub(string auth0Id)
    {
        var user = await _tennisManagerContext.Users.FirstOrDefaultAsync(u => u.Sub == auth0Id);
        return user is null ? null : MapUserDto(user);
    }

    public async Task<UserDto?> CreateUserAsync(UserDto userDto)
    {
        User entity = userDto switch
        {
            CoachDto coachDto => _mapper.Map<Coach>(coachDto),
            CustomerDto customerDto => _mapper.Map<Customer>(customerDto),
            _ => throw new NotImplementedException()
        };

        _tennisManagerContext.Users.Add(entity);
        await _tennisManagerContext.SaveChangesAsync();

        var result = MapUserDto(entity);
        return result;
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _tennisManagerContext.Users.FindAsync(id);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }
    
    private UserDto MapUserDto(User user)
    {
        return user switch
        {
            Coach coach => _mapper.Map<CoachDto>(coach),
            Customer customer => _mapper.Map<CustomerDto>(customer),
            _ => _mapper.Map<UserDto>(user)
        };
    }
}