using Takecontrol.Domain.Primitives;

namespace Takecontrol.Application.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException(DomainError error) : base(error)
    {
    }
}
