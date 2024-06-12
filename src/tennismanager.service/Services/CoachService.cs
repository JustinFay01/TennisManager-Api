using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager.data;
using tennismanager.data.Entities;
using tennismanager.service.DTO;
using tennismanager.shared;

namespace tennismanager.service.Services;

public interface ICoachService
{
    Task<CoachDto> CreateCoachAsync(CoachDto coach);
    
    Task<CoachDto?> GetCoachByIdAsync(Guid id);
    
    Task DeleteUserAsync(Guid id);
    
    Task<PackagePriceDto?> PutPackagePriceAsync(decimal requestPrice, Guid requestCoachId, Guid requestPackageId);
}

public class CoachService : ICoachService
{
    private IMapper _mapper;
    private TennisManagerContext _tennisManagerContext;
    
    public CoachService(IMapper mapper, TennisManagerContext tennisManagerContext)
    {
        _mapper = mapper;
        _tennisManagerContext = tennisManagerContext;
    }

    public async Task<CoachDto> CreateCoachAsync(CoachDto coachDto)
    {
        var coach = _mapper.Map<Coach>(coachDto);

        _tennisManagerContext.Coaches.Add(coach);

        await _tennisManagerContext.SaveChangesAsync();

        return _mapper.Map<CoachDto>(coach);
    }
    
    public async Task<CoachDto?> GetCoachByIdAsync(Guid id)
    {
        var coach = await _tennisManagerContext.Coaches
            .Include(c => c.PackagePricesList)
            .FirstOrDefaultAsync(c => c.Id == id);
        return coach != null ? _mapper.Map<CoachDto>(coach) : null;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        // Find customer
        var customer = await _tennisManagerContext.Customers
            .Include(c => c.Packages)
            .Include(c => c.ParticipatedSessions)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (customer == null)
        {
            // Since the customer was not found, check if the user is a coach
            var user = await _tennisManagerContext.Coaches
                .Include(c => c.PackagePricesList)
                .FirstOrDefaultAsync(c => c.Id == id);
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