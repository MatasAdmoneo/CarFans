using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cf.Domain.Exceptions;

public abstract class BaseException : Exception
{
    /// <summary>
    /// Indicates if ErrorInfo has parameterized message.
    /// </summary>
    /// <example>false</example>
    public bool IsParameterized { get; set; }

    /// <summary>
    /// Contains more detailed information about occurred error.
    /// </summary>
    public MessageInfo ErrorInfo { get; set; }

    /// <summary>
    /// Http Status code.
    /// </summary>
    /// <example>BadRequest</example>
    public abstract HttpStatusCode StatusCode { get; }

    protected BaseException(MessageInfo errorInfo) : base(errorInfo.Message)
    {
        ErrorInfo = errorInfo;
    }
}

