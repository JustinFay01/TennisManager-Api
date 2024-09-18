using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager.data;
using tennismanager.data.Entities;
using tennismanager.service.DTO.Session;
using tennismanager.shared.Models;

namespace tennismanager.service.Services;

public interface ISessionService
{
    Task<SessionDto> CreateSessionAsync(SessionDto sessionDto);
    Task UpdateSessionAsync(SessionDto sessionDto);
    Task AddCustomersToSessionAsync(List<CustomerSessionDto> customerSessions);
    Task<SessionDto?> GetSessionByIdAsync(Guid id);
    Task<PagedResponse<SessionDto>> GetSessionsAsync(int page, int pageSize);
}

public class SessionService : ISessionService
{
    private readonly TennisManagerContext _tennisManagerContext;
    private readonly IMapper _mapper;

    public SessionService(TennisManagerContext tennisManagerContext, IMapper mapper)
    {
        _tennisManagerContext = tennisManagerContext;
        _mapper = mapper;
    }

    public async Task<SessionDto> CreateSessionAsync(SessionDto sessionDto)
    {
        var session = _mapper.Map<Session>(sessionDto);

        _tennisManagerContext.Sessions.Add(session);

        await _tennisManagerContext.SaveChangesAsync();
        
        var mappedSession = _mapper.Map<SessionDto>(session);
        return mappedSession;
    }

    public Task UpdateSessionAsync(SessionDto sessionDto)
    {
        var session = _mapper.Map<Session>(sessionDto);
        _tennisManagerContext.Sessions.Update(session);
        return _tennisManagerContext.SaveChangesAsync();
    }

    public async Task<SessionDto?> GetSessionByIdAsync(Guid id)
    {
        var session = await _tennisManagerContext.Sessions.FirstOrDefaultAsync(s => s.Id == id);
        return _mapper.Map<SessionDto>(session);
    }

    // TODO: Refactor to: a) use a transaction b) return the number of successfully added entities, c) list of failed entities
    public async Task AddCustomersToSessionAsync(List<CustomerSessionDto> customerSessions)
    {
        var entities = _mapper.Map<List<CustomerSession>>(customerSessions);
        await _tennisManagerContext.CustomerSessions.AddRangeAsync(entities);
        await _tennisManagerContext.SaveChangesAsync();
    }

    public async Task<PagedResponse<SessionDto>> GetSessionsAsync(int page, int pageSize)
    {
        var count = _tennisManagerContext.Sessions.Count();

        var query = await _tennisManagerContext.Sessions
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<SessionDto>
        {
            Items = _mapper.Map<List<SessionDto>>(query),
            PageNumber = page,
            PageSize = pageSize,
            TotalItems = count
        };
    }
}