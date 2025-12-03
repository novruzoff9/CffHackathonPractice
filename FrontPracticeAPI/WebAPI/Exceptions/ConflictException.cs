using WebAPI.Exceptions.Common;

namespace WebAPI.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException() : base("Conflict occurred.", 409)
    {
    }
    public ConflictException(string message) : base(message, 409)
    {
    }
    public ConflictException(string message, Exception innerException) : base(message, innerException, 409)
    {
    }
}