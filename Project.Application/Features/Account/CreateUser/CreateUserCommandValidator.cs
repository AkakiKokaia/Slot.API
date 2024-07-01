using FluentValidation;
using Project.Application.Configuration.Exceptions;

namespace Project.Application.Features.Account.CreateUser
{
    public class CreateUserCommandValidator : CustomAbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.userName)
                .NotEmpty();

            RuleFor(v => v.userName)
                .MinimumLength(6)
                .WithMessage("UserName must be at least 6 characters long.");

            RuleFor(v => v.firstName)
                .NotEmpty()
                .WithMessage("First Name is required.");

            RuleFor(v => v.firstName)
                .MinimumLength(2)
                .WithMessage("First Name must be at least 2 characters long.");

            RuleFor(v => v.lastName)
                .NotEmpty()
                .WithMessage("Last Name is required.");

            RuleFor(v => v.lastName)
                .MinimumLength(2)
                .WithMessage("Last Name must be at least 2 characters long.");

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

            RuleFor(v => v.email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
