namespace Slot.Application.Configuration.Exceptions.Types;

public enum ApiExceptionCodeTypes
{
    NotFound = 1400,
    Unhandled = 1500,
    Exists = 1501,
    InsufficientFunds = 1600,
    UserVerification = 1701,
    UserLockedOut = 1702,
    InvalidCredentials = 1703,
    InvalidToken = 1704,
    Validation = 1800,

}
