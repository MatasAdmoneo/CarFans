using System.Net;

namespace Cf.Domain.Exceptions;

public class BadRequestException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public BadRequestException(MessageInfo errorInfo) : base(errorInfo)
    {
    }
}

public class NotFoundException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public NotFoundException(MessageInfo errorInfo) : base(errorInfo)
    {
    }
}

public class InternalException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

    public InternalException(MessageInfo errorInfo) : base(errorInfo)
    {
    }
}

