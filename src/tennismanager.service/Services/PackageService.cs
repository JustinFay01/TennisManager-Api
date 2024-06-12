using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tennismanager_api.tennismanager.data;
using tennismanager_api.tennismanager.data.Entities;
using tennismanager_api.tennismanager.services.DTO;

namespace tennismanager_api.tennismanager.services.Services;

public interface IPackageService
{
    Task<PackageDto> CreatePackageAsync(PackageDto coach);
    Task<PackageDto?> GetPackageByIdAsync(Guid id);
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
    
    
    public async Task<PackageDto> CreatePackageAsync(PackageDto packageDto)
    {
        var package = _mapper.Map<Package>(packageDto);

        _tennisManagerContext.Packages.Add(package);

        await _tennisManagerContext.SaveChangesAsync();
        
        return _mapper.Map<PackageDto>(package);
    }

    public async Task<PackageDto?> GetPackageByIdAsync(Guid id)
    {
        var package = await _tennisManagerContext.Packages.FirstOrDefaultAsync(p => p.Id == id);
        return package != null ? _mapper.Map<PackageDto>(package) : null;
    }
}