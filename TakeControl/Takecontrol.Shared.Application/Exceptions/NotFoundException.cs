using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Shared.Application.Exceptions;

public sealed class NotFoundException : BaseException
{
    public NotFoundException(DomainError error) : base(error)
    {

    }
}
