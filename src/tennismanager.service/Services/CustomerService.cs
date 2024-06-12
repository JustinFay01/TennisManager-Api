using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager.data;
using tennismanager.data.Entities;
using tennismanager.service.DTO;

namespace tennismanager.service.Services;

public interface ICustomerService
{
    Task<CustomerDto> CreateCustomerAsync(CustomerDto coach);
    
    Task<CustomerDto?> GetCustomerByIdAsync(Guid id);
}

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly TennisManagerContext _tennisManagerContext;


    public CustomerService(
        IMapper mapper,
        TennisManagerContext tennisManagerContext
        )
    {
        _mapper = mapper;
        _tennisManagerContext = tennisManagerContext;
    }
    
    public async Task<CustomerDto> CreateCustomerAsync(CustomerDto coach)
    {
        var customer = _mapper.Map<Customer>(coach);
        
        _tennisManagerContext.Customers.Add(customer);
        
        await _tennisManagerContext.SaveChangesAsync();
        
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _tennisManagerContext.Customers
            .Include(c => c.Packages)
            .Include(c => c.ParticipatedSessions)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return customer != null ? _mapper.Map<CustomerDto>(customer) : null;
    }
}