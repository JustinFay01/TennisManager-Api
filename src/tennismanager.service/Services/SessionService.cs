using AutoMapper;
using tennismanager_api.tennismanager.data;
using tennismanager_api.tennismanager.data.Entities;
using tennismanager_api.tennismanager.services.DTO;
using tennismanager.service.DTO;

namespace tennismanager_api.tennismanager.services.Services;

public interface  ISessionService
{
    Task<SessionDto> CreateSessionAsync(SessionDto sessionDto);   
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
}