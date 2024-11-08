using AutoMapper;
using tennismanager.data;
using tennismanager.data.Entities;
using tennismanager.service.DTO;
using tennismanager.service.DTO.Users;
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

    public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);

        if (!string.IsNullOrEmpty(customerDto.PhoneNumber))
            customer.PhoneNumber = ParsePhoneNumber(customer.PhoneNumber);

        _tennisManagerContext.Customers.Add(customer);

        await _tennisManagerContext.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
    {
        // var customer = await _tennisManagerContext.Customers
        //     .Include(c => c.Packages)
        //     .Include(c => c.Sessions)
        //     .FirstOrDefaultAsync(c => c.Id == id);

        // return customer != null ? _mapper.Map<CustomerDto>(customer) : null;
        return null;
    }

    public async Task<PagedResponse<CustomerDto>> GetCustomersAsync(int page, int pageSize)
    {
        return null;
    }

    private static string ParsePhoneNumber(string phoneNumber)
    {
        // Remove all non numbers
        var number = phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
        // add . 
        return number.Insert(3, ".").Insert(7, ".");
    }
}