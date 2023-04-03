using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Shared.Application.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(DomainError error) : base(error)
    {
    }
}
