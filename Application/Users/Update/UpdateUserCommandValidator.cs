using Domain.Users;
using FluentValidation;

namespace Application.Users.Update;

public class UpdateUserCommandValidator: AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(u => u.RoleId)
            .Must(BeAValidRoleId)
            .WithMessage(r => $"There is no role with id {r.RoleId}");
    }

    public static bool BeAValidRoleId(int roleId)
    {
        return Role.TryFromValue(roleId, out var _);
    }
}