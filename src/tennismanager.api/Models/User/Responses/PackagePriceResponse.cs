using System.Text.Json.Serialization;

namespace tennismanager.api.Models.User.Responses;

public class PackagePriceResponse
{
    [JsonPropertyName("price")] 
    public required decimal Price { get; set; }
    
    [JsonPropertyName("packageId")] 
    public string PackageId { get; set; }
}