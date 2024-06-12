using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager.data;
using tennismanager.data.Entities;
using tennismanager.data.Entities.Abstract;
using tennismanager.service.DTO;
using tennismanager.shared;

namespace tennismanager.service.Services;

public interface IUserService
{
    Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto);
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



    public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);

        _tennisManagerContext.Customers.Add(customer);

        await _tennisManagerContext.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var coach = await _tennisManagerContext.Coaches.Include(c => c.PackagePricesList)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (coach != null)
        {
            return _mapper.Map<CoachDto>(coach);
        }

        var customer = await _tennisManagerContext.Customers
            .Include(c => c.Packages)
            .Include(c => c.ParticipatedSessions)
            .FirstOrDefaultAsync(c => c.Id == id);
        return customer != null ? _mapper.Map<CustomerDto>(customer) : null;
    }




}