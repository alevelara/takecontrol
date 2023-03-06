using takecontrol.Domain.Primitives;

namespace takecontrol.Application.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(DomainError error) : base(error)
    {
    }
}
