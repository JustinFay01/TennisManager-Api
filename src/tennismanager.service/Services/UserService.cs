using AutoMapper;
using tennismanager.data;
using tennismanager.data.Entities;
using tennismanager.data.Entities.Abstract;
using tennismanager.service.DTO;

namespace tennismanager.service.Services;

public interface IUserService
{
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

        var result = _mapper.Map<UserDto>(entity);
        return result;
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _tennisManagerContext.Users.FindAsync(id);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }
}