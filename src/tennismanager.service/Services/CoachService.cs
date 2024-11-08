using AutoMapper;
using tennismanager.data;
using tennismanager.service.DTO;
using tennismanager.service.DTO.Users;

namespace tennismanager.service.Services;

public interface ICoachService
{
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

    //TODO: FIX THIS
    public async Task<CoachDto?> GetCoachByIdAsync(Guid id)
    {
        // var coach = await _tennisManagerContext.Coaches
        //     .Include(c => c.PackagePricesList)
        //     .FirstOrDefaultAsync(c => c.Id == id);
        // return coach != null ? _mapper.Map<CoachDto>(coach) : null;
        return null;
    }

    //TODO: FIX THIS
    public async Task DeleteUserAsync(Guid id)
    {
        // // Find customer
        // var customer = await _tennisManagerContext.Customers
        //     .Include(c => c.Packages)
        //     .Include(c => c.Sessions)
        //     .FirstOrDefaultAsync(c => c.Id == id);
        // if (customer == null)
        // {
        //     // Since the customer was not found, check if the user is a coach
        //     var user = await _tennisManagerContext.Coaches
        //         .Include(c => c.PackagePricesList)
        //         .FirstOrDefaultAsync(c => c.Id == id);
        //     if (user == null || user.Id == new Guid(SystemUserIds.JustinFayId))
        //     {
        //         throw new ValidationException("No user found.");
        //     }
        //
        //     _tennisManagerContext.Coaches.Remove(user);
        // }
        // else
        // {
        //     _tennisManagerContext.Customers.Remove(customer);
        // }
        //
        // await _tennisManagerContext.SaveChangesAsync();
    }
}