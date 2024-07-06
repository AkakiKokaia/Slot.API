namespace Slot.Application.Features.Account.SignIn;
public class SignInCommandResponse
{
    public SignInCommandResponse(string token)
    {
        Token = token;
    }

    public string? Token { get; set; }

}
