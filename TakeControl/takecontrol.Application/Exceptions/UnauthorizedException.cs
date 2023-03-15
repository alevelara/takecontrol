using Takecontrol.Domain.Primitives;

namespace Takecontrol.Application.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(DomainError error) : base(error)
    {
    }
}
