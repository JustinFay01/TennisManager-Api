using tennismanager.api.Models.User.Abstract;

namespace tennismanager.api.Models.User.Responses;

public class CoachResponse : UserResponse
{
    //public List<PackagePriceResponse> PackagePricesList { get; set; } = [];
    public decimal HourlyRate { get; set; }
}