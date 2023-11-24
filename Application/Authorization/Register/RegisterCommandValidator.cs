using FluentValidation;

namespace Application.Authorization.Register;

public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .MaximumLength(254)
            .EmailAddress();

        RuleFor(u => u.Username)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(7)
            .MaximumLength(30);
    }
}