using Takecontrol.Domain.Primitives;

namespace Takecontrol.Application.Exceptions;

public sealed class BadRequestException : BaseException
{
    public BadRequestException(DomainError error) : base(error)
    {
    }
}
