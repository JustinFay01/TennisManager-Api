using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager_api.tennismanager.data;
using tennismanager_api.tennismanager.data.Entities;
using tennismanager_api.tennismanager.data.Entities.Abstract;
using tennismanager_api.tennismanager.services.DTO;

namespace tennismanager.service.Services;

public interface IUserService
{
    Task CreateCoachAsync(CoachDto coach);
    Task CreateCustomerAsync(CustomerDto customerDto);
    Task<UserDto?> GetUserByIdAsync(Guid id);
}

public class UserService : IUserService
{
    private readonly TennisManagerContext _tennisManagerContext;
    private readonly IMapper _mapper;


    public UserService(
    TennisManagerContext tennisManagerContext,
    IMapper mapper
    )
    {
        _tennisManagerContext = tennisManagerContext;
        _mapper = mapper;
    }
    
    public async Task CreateCoachAsync(CoachDto coachDto)
    {
        var coach = _mapper.Map<Coach>(coachDto);
        
        _tennisManagerContext.Coaches.Add(coach);
        
        await _tennisManagerContext.SaveChangesAsync();
    }

    public Task CreateCustomerAsync(CustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        
        _tennisManagerContext.Customers.Add(customer);
        
        return _tennisManagerContext.SaveChangesAsync();
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        User? user = await _tennisManagerContext.Coaches.FirstOrDefaultAsync(c => c.Id == id);
        if (user != null)
        {
            return _mapper.Map<CoachDto>(user);
        }

        user = await _tennisManagerContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (user != null)
        {
            return _mapper.Map<CustomerDto>(user);
        }
        
        return null;
    }
}