using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager.data;
using tennismanager.data.Entities;
using tennismanager.service.DTO;
using tennismanager.shared.Models;

namespace tennismanager.service.Services;

public interface ICustomerService
{
    Task<CustomerDto> CreateCustomerAsync(CustomerDto coach);
    
    Task<CustomerDto?> GetCustomerByIdAsync(Guid id);
    
    Task<PagedResponse<CustomerDto>> GetCustomersAsync(int page, int pageSize);
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

    public async Task<PagedResponse<CustomerDto>> GetCustomersAsync(int page, int pageSize)
    {
        var count = await _tennisManagerContext.Customers.CountAsync();

        var query = await _tennisManagerContext.Customers
            .AsNoTracking()
            .Include(c => c.Packages)
            .Include(c => c.ParticipatedSessions)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedResponse<CustomerDto>
        {
            Items = _mapper.Map<List<CustomerDto>>(query),
            TotalItems = count,
            PageNumber = page,
            PageSize = pageSize
            // Automatically calculates the TotalPages
        };
    }
}