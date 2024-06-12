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
    Task<CoachDto> CreateCoachAsync(CoachDto coach);
    Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto);
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task DeleteUserAsync(Guid id);
    Task<PackagePriceDto?> PutPackagePriceAsync(decimal requestPrice, Guid requestCoachId, Guid requestPackageId);
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

    public async Task DeleteUserAsync(Guid id)
    {
        // Find customer
        var customer = await _tennisManagerContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer == null)
        {
            // Since the customer was not found, check if the user is a coach
            var user = await _tennisManagerContext.Coaches.FirstOrDefaultAsync(c => c.Id == id);
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

    public async Task<PackagePriceDto?> PutPackagePriceAsync(decimal requestPrice, Guid requestCoachId,
        Guid requestPackageId)
    {
        // Determine if the coach exists
        var coach = await _tennisManagerContext.Coaches
            .Include(x => x.PackagePricesList)
            .FirstOrDefaultAsync(c => c.Id == requestCoachId);
        if (coach == null)
        {
            throw new ValidationException("No coach found.");
        }

        // Determine if the package exists
        var package = await _tennisManagerContext.Packages.FirstOrDefaultAsync(p => p.Id == requestPackageId);
        if (package == null)
        {
            throw new ValidationException("No package found.");
        }
        
        if(coach.PackagePricesList.Any(x => x.PackageId == requestPackageId))
        {
            throw new ValidationException("Package price already exists.");
        }

        // TODO: Implement auto price calculation
        //var price = requestPrice == 0 ? package.Uses - 1 * coach.DefaultPrice : requestPrice;
        // Create a new package price
        var packagePrice = new CoachPackagePrice
        {
            Price = requestPrice,
            CoachId = requestCoachId,
            PackageId = requestPackageId
        };

        // Add to customer
        coach.PackagePricesList.Add(packagePrice);

        // Save changes
        await _tennisManagerContext.SaveChangesAsync();

        return _mapper.Map<PackagePriceDto>(packagePrice);
    }
}