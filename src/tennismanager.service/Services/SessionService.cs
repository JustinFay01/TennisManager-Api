using AutoMapper;
using tennismanager_api.tennismanager.data;
using tennismanager_api.tennismanager.data.Entities;
using tennismanager_api.tennismanager.services.DTO;

namespace tennismanager_api.tennismanager.services.Services;

public interface  ISessionService
{
    Task CreateSessionAsync(SessionDto sessionDto);   
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
    
    public Task CreateSessionAsync(SessionDto sessionDto)
    {
        var session = _mapper.Map<Session>(sessionDto);
        
        _tennisManagerContext.Sessions.Add(session);
        
        return _tennisManagerContext.SaveChangesAsync();
    }
}