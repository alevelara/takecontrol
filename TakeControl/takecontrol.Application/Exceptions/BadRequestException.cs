using takecontrol.Domain.Primitives;

namespace takecontrol.Application.Exceptions;

public sealed class BadRequestException : BaseException
{
    public BadRequestException(DomainError error) : base(error)
    {
    }
}
