using System.Text.Json.Serialization;
using FluentValidation;
using tennismanager.api.Extensions;

namespace tennismanager.api.Models.Session;

public class SessionAddCustomersRequest
{
    [JsonPropertyName("sessionIds")]
    public List<string> SessionIds { get; set; }
    
    [JsonPropertyName("customersAndPrices")]
    public Dictionary<string, decimal> CustomersAndPrices { get; set; }
}

public class SessionAddCustomersRequestValidator : AbstractValidator<SessionAddCustomersRequest>
{
    public SessionAddCustomersRequestValidator()
    {
        RuleFor(x => x.SessionIds).NotEmpty()
            .ForEach(x => x.IsValidGuid());
        RuleFor(x => x.CustomersAndPrices).NotEmpty();

        RuleForEach(x => x.CustomersAndPrices)
            .ChildRules(c =>
            {
                c.RuleFor(x => x.Key).IsValidGuid();
                c.RuleFor(x => x.Value).NotNull();
            });
    }
}