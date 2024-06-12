using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager_api.tennismanager.data.Entities;
using tennismanager_api.tennismanager.data.Entities.Abstract;
using tennismanager_api.tennismanager.services.DTO;
using tennismanager.data;
using tennismanager.shared;

namespace tennismanager.service.Services;

public interface IUserService
{
    Task<CoachDto> CreateCoachAsync(CoachDto coach);
    Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto);
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task DeleteUserAsync(Guid coachId, Guid userId);
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
    
    public async Task<CoachDto> CreateCoachAsync(CoachDto coachDto)
    {
        var coach = _mapper.Map<Coach>(coachDto);
        
        _tennisManagerContext.Coaches.Add(coach);
        
        await _tennisManagerContext.SaveChangesAsync();
        
        return _mapper.Map<CoachDto>(coach);
    }

    public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        
        _tennisManagerContext.Customers.Add(customer);
        
        await _tennisManagerContext.SaveChangesAsync();
        
        return _mapper.Map<CustomerDto>(customer);
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

    public async Task DeleteUserAsync(Guid coachId, Guid userId)
    {
        // Determine if coachId is a coach or customer
        var coach = await _tennisManagerContext.Coaches.FirstOrDefaultAsync(c => c.Id == coachId);
        if (coach == null)
        {
            throw new ValidationException("Only a coach can delete a user");
        }
        
        // Find customer
        var customer = await _tennisManagerContext.Customers.FirstOrDefaultAsync(c => c.Id == userId);
        if (customer == null)
        {
            // Since the customer was not found, check if the user is a coach
            var user = await _tennisManagerContext.Coaches.FirstOrDefaultAsync(c => c.Id == userId);
            if (user == null || user.Id == new Guid(SystemUserIds.JustinFayId))
            {
                throw new ValidationException("No user found.");
            }

            _tennisManagerContext.Coaches.Remove(user);
        }
        else
        {
            _tennisManagerContext.Customers.Remove(customer);
        }
        
        await _tennisManagerContext.SaveChangesAsync();
    }
}