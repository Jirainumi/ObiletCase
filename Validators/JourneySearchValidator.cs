using FluentValidation;
using ObiletCase.Constants;

namespace ObiletCase.Validators;

/// <summary>
/// Journey search parameters validator
/// </summary>
public class JourneySearchValidator : AbstractValidator<JourneySearchParameters>
{
    public JourneySearchValidator()
    {
        RuleFor(x => x.OriginId)
            .GreaterThan(0)
            .WithMessage(ErrorMessages.LocationNotFound);

        RuleFor(x => x.DestinationId)
            .GreaterThan(0)
            .WithMessage(ErrorMessages.LocationNotFound);

        RuleFor(x => x.OriginId)
            .NotEqual(x => x.DestinationId)
            .WithMessage(ErrorMessages.SameLocationError);

        RuleFor(x => x.Date)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage(ErrorMessages.InvalidDate);
    }
}

/// <summary>
/// Journey search parameters
/// </summary>
public class JourneySearchParameters
{
    public int OriginId { get; set; }
    public int DestinationId { get; set; }
    public DateTime Date { get; set; }
}