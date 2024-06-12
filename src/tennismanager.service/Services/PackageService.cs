using AutoMapper;
using tennismanager_api.tennismanager.data;
using tennismanager_api.tennismanager.data.Entities;
using tennismanager_api.tennismanager.services.DTO;

namespace tennismanager_api.tennismanager.services.Services;

public interface IPackageService
{
    Task CreatePackageAsync(PackageDto coach);
}
public class PackageService : IPackageService
{
    private readonly TennisManagerContext _tennisManagerContext;
    private readonly IMapper _mapper;


    public PackageService(
        TennisManagerContext tennisManagerContext,
        IMapper mapper
    )
    {
        _tennisManagerContext = tennisManagerContext;
        _mapper = mapper;
    }
    
    
    public async Task CreatePackageAsync(PackageDto packageDto)
    {
        var package = _mapper.Map<Package>(packageDto);

        _tennisManagerContext.Packages.Add(package);

        await _tennisManagerContext.SaveChangesAsync();
    }
}