using takecontrol.Domain.Primitives;

namespace takecontrol.Application.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException(DomainError error) : base(error)
    {
    }
}
