using FluentValidation;
using Project.Application.Configuration.Exceptions;
using Project.Application.Features.Profile.UpdateProfile;

namespace Project.Application.Features.Profile.Update;
public class UpdateProfileCommandValidator : CustomAbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
        RuleFor(x => x.firstName)
            .NotEmpty()
            .WithMessage("FirstName is Required");

        RuleFor(x => x.lastName)
            .NotEmpty()
            .WithMessage("LastName is Required");
    }
}
