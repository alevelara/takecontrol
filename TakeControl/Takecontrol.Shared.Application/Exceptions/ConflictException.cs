using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Shared.Application.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException(DomainError error) : base(error)
    {
    }
}
