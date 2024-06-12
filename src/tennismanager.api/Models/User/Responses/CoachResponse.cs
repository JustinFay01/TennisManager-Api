using tennismanager.api.Models.User.Abstract;

namespace tennismanager.api.Models.User.Responses;

public class CoachResponse : UserResponse
{
    public ICollection<PackagePriceResponse> PackagePricesList { get; set; } = new List<PackagePriceResponse>();
}