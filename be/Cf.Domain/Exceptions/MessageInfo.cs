
namespace Cf.Domain.Exceptions;

public class MessageInfo
{
    public string? Message { get; set; }

    public MessageInfo(string? message)
    {
        Message = message;
    }
}

