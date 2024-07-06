using FluentValidation;
using Slot.Application.Configuration.Exceptions;
using Slot.Application.Features.Profile.UpdateProfile;

namespace Slot.Application.Features.Profile.Update;
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
