﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager.data;
using tennismanager.data.Entities;
using tennismanager.service.DTO.Session;
using tennismanager.shared.Models;

namespace tennismanager.service.Services;

public interface ISessionService
{
    Task<SessionDto> CreateSessionAsync(SessionDto sessionDto);
    Task UpdateSessionAsync(Guid id, SessionDto sessionDto);
    Task AddCustomersToSessionAsync(List<Guid> sessionIds, Dictionary<Guid, decimal> customerIds);
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

    public Task UpdateSessionAsync(Guid id, SessionDto sessionDto)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto?> GetSessionByIdAsync(Guid id)
    {
        var session = await _tennisManagerContext.Sessions.FirstOrDefaultAsync(s => s.Id == id);
        return _mapper.Map<SessionDto>(session);
    }

    public async Task AddCustomersToSessionAsync(List<Guid> sessionIds, Dictionary<Guid, decimal> customerIds)
    {
        var customerSessions = new List<CustomerSession>();
        foreach (var id in sessionIds)
        {
            // customerSessions.AddRange(customerIds
            //     .Select(kvp => new CustomerSession
            //     {
            //         CustomerId = kvp.Key,
            //         SessionId = id,
            //         Price = kvp.Value
            //     })
            //     .ToList());
        }

        await _tennisManagerContext.CustomerSessions.AddRangeAsync(customerSessions);
        await _tennisManagerContext.SaveChangesAsync();
    }

    public async Task<PagedResponse<SessionDto>> GetSessionsAsync(int page, int pageSize)
    {
        var count = _tennisManagerContext.Sessions.Count();

        // var query = await _tennisManagerContext.Sessions
        //     .AsNoTracking()
        //     .OrderByDescending(s => s.Date)
        //     .Skip((page - 1) * pageSize)
        //     .Take(pageSize)
        //     .ToListAsync();

        return new PagedResponse<SessionDto>
        {
            // Items = _mapper.Map<List<SessionDto>>(query),
            // TotalItems = count,
            // PageNumber = page,
            // PageSize = pageSize
        };
    }
}