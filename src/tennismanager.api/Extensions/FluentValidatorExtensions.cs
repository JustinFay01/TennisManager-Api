using FluentValidation;

namespace tennismanager.api.Extensions;

public static class FluentValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> IsValidGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(guidString => Guid.TryParse(guidString, out _))
            .WithMessage("The field must be a valid GUID.");
    }
}