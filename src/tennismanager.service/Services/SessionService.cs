using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager.data;
using tennismanager.data.Entities;
using tennismanager.service.DTO.Session;
using tennismanager.shared.Exceptions.Exceptions;
using tennismanager.shared.Models;

namespace tennismanager.service.Services;

public interface ISessionService
{
    Task<SessionDto> CreateSessionAsync(SessionDto sessionDto);
    Task UpdateSessionAsync(SessionDto sessionDto);
    Task<SessionDto?> GetSessionByIdAsync(Guid id);
    Task<PagedResponse<SessionDto>> GetSessionsAsync(int? page, int? pageSize);
    Task DeleteSessionAsync(Guid id);
    
    Task AddCustomersToSessionAsync(List<CustomerSessionDto> customerSessions);
}

public class SessionService : ISessionService
{
    private readonly IMapper _mapper;
    private readonly TennisManagerContext _tennisManagerContext;

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
    public Task DeleteSessionAsync(Guid id)
    {
        var session = _tennisManagerContext.Sessions.FirstOrDefault(s => s.Id == id);
        if (session == null) throw new SessionNotFoundException();
        _tennisManagerContext.Sessions.Remove(session);
        
        return _tennisManagerContext.SaveChangesAsync();
    }

    public async Task AddCustomersToSessionAsync(List<CustomerSessionDto> customerSessions)
    {
        var entities = _mapper.Map<List<CustomerSession>>(customerSessions);
        await _tennisManagerContext.CustomerSessions.AddRangeAsync(entities);
        await _tennisManagerContext.SaveChangesAsync();
    }

    public async Task<PagedResponse<SessionDto>> GetSessionsAsync(int? page, int? pageSize)
    {
        IQueryable<Session> query = _tennisManagerContext.Sessions
            .Include(session => session.SessionMeta)
            .ThenInclude(meta => meta.SessionIntervals) 
            .Include(session => session.CustomerSessions);

        if(page != null && pageSize != null)
        {
            query = query.Skip((int) pageSize * ((int) page - 1)).Take((int) pageSize);
        }
        
        var count = await query.CountAsync();
        
        return new PagedResponse<SessionDto>(page ?? 1, pageSize ?? count)
        {
            Items = _mapper.Map<List<SessionDto>>(query),
            TotalItems = count
        };
    }
}