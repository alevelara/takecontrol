using Takecontrol.Domain.Primitives;

namespace Takecontrol.Application.Exceptions;

public sealed class NotFoundException : BaseException
{
    public NotFoundException(DomainError error) : base(error)
    {
    }
}
