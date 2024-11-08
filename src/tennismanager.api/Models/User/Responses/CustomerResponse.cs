using tennismanager.api.Models.Session.Responses;
using tennismanager.api.Models.User.Abstract;

namespace tennismanager.api.Models.User.Responses;

public class CustomerResponse : UserResponse
{
    public List<CustomerSessionResponse> Sessions { get; set; } = [];
}