using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager_api.tennismanager.data;
using tennismanager_api.tennismanager.data.Entities;
using tennismanager.data;
using tennismanager.service.DTO;

namespace tennismanager.service.Services;

public interface  ISessionService
{
    Task<SessionDto> CreateSessionAsync(SessionDto sessionDto);  
    
    Task<SessionDto?> GetSessionByIdAsync(Guid id);
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
         
         return _mapper.Map<SessionDto>(session);
    }

    public async Task<SessionDto?> GetSessionByIdAsync(Guid id)
    {
        var session = await _tennisManagerContext.Sessions.FirstOrDefaultAsync(s => s.Id == id);
        return session != null ? _mapper.Map<SessionDto>(session) : null;
    }
}