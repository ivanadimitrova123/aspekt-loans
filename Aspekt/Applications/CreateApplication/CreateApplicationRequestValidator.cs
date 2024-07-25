namespace Aspekt.Applications.CreateApplication;

using FluentValidation;

public sealed class CreateApplicationRequestValidator : AbstractValidator<CreateApplicationRequest>
{
    public CreateApplicationRequestValidator()
    {
        RuleFor(request => request.CountactId).NotEmpty();
        RuleFor(request => request.Amount).GreaterThan(0);
        RuleFor(request => request.PreparedAt).NotEmpty();
    }
}