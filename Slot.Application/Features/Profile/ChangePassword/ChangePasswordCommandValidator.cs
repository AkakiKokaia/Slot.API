using FluentValidation;
using Slot.Application.Configuration.Exceptions;

namespace Slot.Application.Features.Profile.ChangePassword;

public class ChangePasswordCommandValidator : CustomAbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(v => v.currentPassword).NotEmpty().WithMessage("Password is required.");

        RuleFor(v => v.currentPassword).MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.");

        RuleFor(v => v.currentPassword).Matches(@"\d")
            .WithMessage("Password must contain at least one digit.");

        RuleFor(v => v.currentPassword).Matches(@"[a-z]")
            .WithMessage("Password must contain at least one lowercase letter.");

        RuleFor(v => v.currentPassword).Matches(@"[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.");

        RuleFor(v => v.currentPassword).Matches(@"\W")
            .WithMessage("Password must contain at least one non-alphanumeric character.");

        RuleFor(v => v.password).NotEmpty().WithMessage("Password is required.");

        RuleFor(v => v.password).MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.");

        RuleFor(v => v.password).Matches(@"\d")
            .WithMessage("Password must contain at least one digit.");

        RuleFor(v => v.password).Matches(@"[a-z]")
            .WithMessage("Password must contain at least one lowercase letter.");

        RuleFor(v => v.password).Matches(@"[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.");

        RuleFor(v => v.password).Matches(@"\W")
            .WithMessage("Password must contain at least one non-alphanumeric character.");

        RuleFor(v => v.confirmPassword)
            .NotEmpty()
            .Equal(v => v.password);
    }
}
