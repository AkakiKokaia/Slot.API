using FluentValidation;
using Project.Application.Configuration.Exceptions;

namespace Project.Application.Features.Account.SignIn;

public class SignInCommandValidator : CustomAbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.username)
            .NotEmpty()
            .WithMessage("UserName is Required!");

        RuleFor(x => x.password)
            .NotEmpty()
            .WithMessage("Password is required!");
    }
}
